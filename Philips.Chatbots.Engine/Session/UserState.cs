using Microsoft.Bot.Builder;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Common;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Database.MongoDB;
using Philips.Chatbots.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        End = 1,
        InvalidInput = 2,
        RecordFeedback = 3,
        SolutionFound = 4,
        ExpInput = 5,
        PickNode = 6
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

        public NeuraLinkModel CurrentLink
        {
            get => currentLink; set
            {
                LinkHistory.Push(value);
                currentState = ChatStateType.Start;
                currentLink = value;
            }
        }

        public NeuraLinkModel RootLink => rootLink;

        public Stack<NeuraLinkModel> LinkHistory { get; set; } = new Stack<NeuraLinkModel>();

        public IRequestPipeline RequestPipeline { get; set; }

        public RequestState()
        {

        }

        public async Task Initilize(string userId, string botId, IRequestPipeline pipeline)
        {
            _botId = botId;
            UserId = userId;
            RequestPipeline = pipeline;
            var rootId = await DbBotCollection.GetRootById(BotId);
            CurrentLink = await DbLinkCollection.FindOneById(rootId ?? throw new InvalidOperationException($"Root does not exists for bot: {botId}"));
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
                        var resources = await DbBotCollection.GetFieldValue(BotId, m => m.Configuration.ResourceStrings);
                        var message = resources.FirstOrDefault(item => item.Key == BotResourceKeyConstants.ThankYou).Value ?? "Thank you:)";
                        await turnContext.SendActivityAsync(message);
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
