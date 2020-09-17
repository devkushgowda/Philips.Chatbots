using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Engine.Session;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Philips.Chatbots.Engine.Request.Extensions
{
    /// <summary>
    /// Extension methods for AlphaRequestHandler.cs
    /// </summary>
    public static class AlphaActivitiesExtension
    {
        /// <summary>
        /// Handles resuest flow for neural resource nodes.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="turnContext"></param>
        /// <param name="requestState"></param>
        /// <returns></returns>
        public async static Task<List<Activity>> ResourceRespose(this NeuralResourceModel resource, ITurnContext turnContext, RequestState requestState)
        {
            var res = new List<Activity>();

            return res;
        }

        /// <summary>
        /// Handles resuest flow for action resource nodes.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="turnContext"></param>
        /// <param name="requestState"></param>
        /// <returns></returns>
        public async static Task<List<Activity>> ActionRespose(this NeuralActionModel resource, ITurnContext turnContext, RequestState requestState)
        {
            var res = new List<Activity>();

            return res;
        }

        /// <summary>
        /// Handles resuest flow for neural expression nodes.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="turnContext"></param>
        /// <param name="requestState"></param>
        /// <returns></returns>
        public async static Task<List<Activity>> ExpressionResponse(this INeuralExpression resource, ITurnContext turnContext, RequestState requestState)
        {
            var res = new List<Activity>();

            return res;
        }
    }
}
