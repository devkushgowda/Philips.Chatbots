using log4net;
using Microsoft.Bot.Builder;
using Philips.Chatbots.Common.Logging;
using Philips.Chatbots.Engine.Interfaces;
using Philips.Chatbots.Engine.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Philips.Chatbots.Engine.Request
{
    /// <summary>
    /// Request pipeline class which allows flexible pipeline definition for handling bot requests.
    /// </summary>
    public class RequestPipeLine : IRequestPipeline
    {
        private static ILog logger = LogHelper.GetLogger<RequestPipeLine>();

        /// <summary>
        /// Pipeline collection.
        /// </summary>
        public List<IRequestHandler> Pipeline { get; set; } = new List<IRequestHandler>();

        /// <summary>
        /// Executes pipeline handler collection one-by-one.
        /// </summary>
        /// <param name="turnContext"></param>
        /// <param name="requestState"></param>
        /// <returns></returns>
        public async Task<PipelineResponse> Execute(ITurnContext turnContext, RequestState requestState)
        {
            var res = new PipelineResponse();
            foreach (var handler in Pipeline)
            {
                try
                {
                    res.Result = await handler.Execute(turnContext, requestState);
                }
                catch(Exception e)
                {
                    logger.Error(e);
                    res.Result = ResponseType.Error;
                }
                res.Count++;
                if (res.Result != ResponseType.Continue)
                    break;
            }
            return res;
        }
    }
}
