using Microsoft.Bot.Builder;
using Microsoft.Extensions.ML;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Engine.Interfaces;
using Philips.Chatbots.Engine.Request.Extensions;
using Philips.Chatbots.ML.Interfaces;
using Philips.Chatbots.ML.Models;
using Philips.Chatbots.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Philips.Chatbots.Database.Common.DbAlias;

namespace Philips.Chatbots.Engine.Session
{
    /// <summary>
    /// Request current state.
    /// </summary>
    public enum ChatStateType
    {
        Start = 0,
        InvalidInput = 1,
        RecordFeedback = 2,
        ExpInput = 3,
        PickNode = 4,
        AdvanceChat = 5
    }

    /// <summary>
    /// User request and session state.
    /// </summary>
    public class RequestState
    {
        private NeuralLinkModel _rootLink;
        private NeuralLinkModel _currentLink;
        private ChatStateType _currentState = ChatStateType.Start;
        private string _botId;
        private string _userId;
        private PredictionEnginePool<NeuralTrainInput, PredictionOutput> _predictionEnginePool;
        private IRequestPipeline _requestPipeline;
        private Stack<NeuralLinkModel> _linkHistory = new Stack<NeuralLinkModel>();

        public Stack<NeuralLinkModel> LinkHistory { get => _linkHistory; set => _linkHistory = value; }

        public ChatStateType CurrentState { get => _currentState; set => _currentState = value; }

        public string BotId => _botId;

        public string UserId => _userId;

        public NeuralLinkModel CurrentLink => _currentLink;

        public NeuralLinkModel RootLink => _rootLink;

        public IRequestPipeline RequestPipeline => _requestPipeline;

        public RequestState()
        {

        }

        public bool StepBack()
        {
            if (LinkHistory.Count < 2)  //Ignore root
                return false;

            NeuralLinkModel top;
            bool res = LinkHistory.TryPop(out top);
            if (res)
            {
                _currentLink = top;
                CurrentState = ChatStateType.Start;
            }
            return res;
        }

        public void StepForward(NeuralLinkModel link, bool recordHistory = true)
        {
            if (recordHistory)
                LinkHistory.Push(link);
            _currentLink = link;
            CurrentState = ChatStateType.Start;
        }

        public string PredictNode(string input) => _predictionEnginePool.Predict(nameof(NeuralPredictionEngine), new NeuralTrainInput { Text = input })._id;

        public async Task Initilize(string userId, string botId, IRequestPipeline requestPipeline, PredictionEnginePool<NeuralTrainInput, PredictionOutput> predictionEnginePool)
        {
            _botId = botId ?? throw new ArgumentNullException(nameof(botId));
            _userId = userId ?? throw new ArgumentNullException(nameof(userId));
            _requestPipeline = requestPipeline ?? throw new ArgumentNullException(nameof(requestPipeline));
            _predictionEnginePool = predictionEnginePool ?? throw new ArgumentNullException(nameof(predictionEnginePool));
            var chatProfile = await CurrentChatProfile();
            _currentLink = await DbLinkCollection.FindOneById(chatProfile?.Root ?? throw new InvalidOperationException($"Root does not exists for bot: {botId}"));
            _rootLink = CurrentLink;
            LinkHistory.Push(CurrentLink);
        }

        public async Task<int> HandleRequest(ITurnContext turnContext)
        {
            var res = await RequestPipeline.Execute(turnContext, this);
            switch (res.Result)
            {
                case ResponseType.End:
                    {
                        await this.RemoveUserState();
                        var reply = turnContext.Activity.CreateReply(StringsProvider.TryGet(BotResourceKeyConstants.ThankYou));
                        reply.SuggestedActions = SuggestionExtension.GetFeedbackSuggestionActions(StringsProvider.TryGet(BotResourceKeyConstants.StartAgain));
                        await turnContext.SendActivityAsync(reply);
                    }
                    break;
                case ResponseType.Error:
                    break;
                case ResponseType.Continue:
                    await this.UpdateUserState();   //Update object in session storage
                    break;
                default:
                    break;
            }
            return res.Count;
        }


    }
}
