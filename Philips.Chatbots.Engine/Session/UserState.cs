using Microsoft.Bot.Builder;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Engine.Interfaces;
using Philips.Chatbots.Engine.Request.Extensions;
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
        PickNode = 4
    }

    /// <summary>
    /// User request and session state.
    /// </summary>
    public class RequestState
    {
        private NeuraLinkModel rootLink;
        private NeuraLinkModel currentLink;
        private ChatStateType currentState = ChatStateType.Start;
        private string _botId;


        public string BotId => _botId;

        public string UserId { get; set; }

        public ChatStateType CurrentState { get => currentState; set => currentState = value; }

        public NeuraLinkModel CurrentLink => currentLink;

        public NeuraLinkModel RootLink => rootLink;

        public Stack<NeuraLinkModel> LinkHistory { get; set; } = new Stack<NeuraLinkModel>();

        public IRequestPipeline RequestPipeline { get; set; }

        public RequestState()
        {

        }

        public bool StepBack()
        {
            if (LinkHistory.Count < 2)  //Ignore root
                return false;

            NeuraLinkModel top;
            bool res = LinkHistory.TryPop(out top);
            if (res)
            {
                currentLink = top;
                CurrentState = ChatStateType.Start;
            }
            return res;
        }

        public void StepForward(NeuraLinkModel link, bool recordHistory = true)
        {
            if (recordHistory)
                LinkHistory.Push(link);
            currentLink = link;
            CurrentState = ChatStateType.Start;
        }

        public async Task Initilize(string userId, string botId, IRequestPipeline pipeline)
        {
            _botId = botId;
            UserId = userId;
            RequestPipeline = pipeline;
            var rootId = await DbBotCollection.GetRootById(BotId);
            currentLink = await DbLinkCollection.FindOneById(rootId ?? throw new InvalidOperationException($"Root does not exists for bot: {botId}"));
            rootLink = CurrentLink;
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
