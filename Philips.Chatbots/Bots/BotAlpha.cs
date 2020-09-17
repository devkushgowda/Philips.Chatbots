using System.Threading;
using System.Threading.Tasks;
using log4net;
using Microsoft.Bot.Builder;
using Philips.Chatbots.Common.Logging;
using Philips.Chatbots.Engine.Session;
using Philips.Chatbots.Engine.Test;

namespace Philips.Chatbots.Bots
{
    /// <summary>
    /// Bot Alpha.
    /// </summary>
    public class BotAlpha : IBot
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private static ILog log = LogHelper.GetLogger<BotAlpha>();

        /// <summary>
        /// Id used to access the bot configuration in DB.
        /// </summary>
        public static string Id => typeof(BotAlpha).FullName;

        /// <summary>
        /// Must define function imposed by the bot framework.
        /// </summary>
        /// <param name="turnContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await BotDbTestClass.Feed(BotAlpha.Id); //Recreates DB in first request, Comment if you want to retain old data.
            var requestState = await turnContext.GetOrCreateUserState(BotAlpha.Id);
            await requestState.HandleRequest(turnContext);
        }
    }

}


