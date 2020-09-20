using log4net;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Philips.Chatbots.Common.Logging;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Engine.Requst.Handlers;
using Philips.Chatbots.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Philips.Chatbots.Database.Common.DbAlias;


namespace Philips.Chatbots.Engine.Request.Extensions
{
    /// <summary>
    /// Extension methods for AlphaRequestHandler.cs
    /// </summary>
    public static class AlphaActivitiesExtension
    {
        public static readonly ILog logger = LogHelper.GetLogger<AlphaRequestHandler>();
        /// <summary>
        /// Handles resuest flow for neural resource nodes.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="turnContext"></param>
        /// <param name="requestState"></param>
        /// <returns></returns>
        public static Attachment BuildResourceAttachment(this NeuralResourceModel resourceModel)
        {
            Attachment res = null;

            switch (resourceModel.Type)
            {
                case ResourceType.DocumentPDF:
                case ResourceType.Text:
                case ResourceType.ImagePNG:
                case ResourceType.ImageJPG:
                case ResourceType.ImageGIF:
                case ResourceType.WebsiteUrl:
                case ResourceType.Audio:
                case ResourceType.Script:
                case ResourceType.Json:
                    {
                        res = new Attachment();
                        res.ContentType = resourceModel.GetAttachmentType();
                        res.ContentUrl = resourceModel.Location;
                    }
                    break;
                case ResourceType.Video:
                    {
                        res = new VideoCard(media: new[] { new MediaUrl(resourceModel.Location) }).ToAttachment();
                    }
                    break;
                default:
                    break;
            }

            return res;
        }

        /// <summary>
        /// Handles resuest flow for action resource nodes.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="turnContext"></param>
        /// <param name="requestState"></param>
        /// <returns></returns>
        public static async Task<List<Activity>> BuildActionRespose(this NeuralActionModel actionModel, ITurnContext turnContext)
        {
            List<Activity> activity = new List<Activity>() { turnContext.Activity.CreateReply(actionModel.ApplyFormat(actionModel.Title)) };
            foreach (var resId in actionModel.Resources)
            {
                try
                {
                    var res = await DbResourceCollection.FindOneById(resId);
                    var act = turnContext.Activity.CreateReply(res.ApplyFormat(res.Title));
                    act.Attachments = new List<Attachment>() { res.BuildResourceAttachment() };
                    activity.Add(act);
                }
                catch (Exception e)
                {
                    logger.Error(e);
                }
            }
            return activity;
        }

    }
}
