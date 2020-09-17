using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Philips.Chatbots.Engine.Interfaces;
using Philips.Chatbots.Engine.Request;
using Philips.Chatbots.Engine.Requst.Handlers;

namespace Philips.Chatbots.Engine.Session
{
    /// <summary>
    /// Session storage class.
    /// </summary>
    public static class SessionStorage
    {
        private static List<RequestState> _userStates = new List<RequestState>();

        private static readonly IRequestPipeline RequestPipeline = new RequestPipeLine { Pipeline = new List<IRequestHandler> { new AlphaRequestHandler() } };

        public async static Task<RequestState> GetOrCreateUserState(this ITurnContext userContext, string botId)
        {
            var id = "AnyId";// userContext.Activity.Id;
            var res = _userStates.FirstOrDefault(item => item.UserId == id);
            if (res == null)
            {
                res = new RequestState();
                await res.Initilize(id, botId, RequestPipeline);
                _userStates.Add(res);
            }
            return res;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async static Task UpdateUserState(this RequestState state)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            //Now not required as the state gets automatically updated.
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async static Task RemoveUserState(this RequestState state)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            _userStates.Remove(state);
        }
    }
}
