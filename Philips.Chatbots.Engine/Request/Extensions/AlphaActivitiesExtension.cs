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
using System.Linq;
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
        /// Handles resuest flow for action resource nodes.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="turnContext"></param>
        /// <param name="requestState"></param>
        /// <returns></returns>
        public static Activity BuildActionRespose(this NeuralActionModel actionModel, ITurnContext turnContext)
        {
            Activity activity = turnContext.Activity.CreateReply(actionModel.ApplyFormat(actionModel.Title));
            List<NeuralResourceModel> resources = actionModel.Resources.Select(async resId => await DbResourceCollection.FindOneById(resId)).Select(task => task.Result).ToList();

            activity.Attachments = resources.Select(res =>
            {
                Attachment attachment = null;
                string curDir = Environment.CurrentDirectory;
                if (res.IsLocal)
                {
                    res.Location = System.IO.Path.Combine(curDir, "resources", res.Location);
                }
                switch (res.Type)
                {
                    case ResourceType.ImagePNG:
                    case ResourceType.ImageJPG:
                    case ResourceType.ImageGIF:
                        {                           
                            attachment = new HeroCard
                            {
                                Title = res.Title,
                                Images = new List<CardImage> { new CardImage { Url = res.Location } }
                            }.ToAttachment();
                        }
                        break;
                    case ResourceType.Audio:
                        {
                            var audioCard = new AudioCard(media: new[] { new MediaUrl(res.Location) });
                            audioCard.Title = res.Title;
                            attachment = audioCard.ToAttachment();
                        }
                        break;
                    case ResourceType.Script:
                    case ResourceType.DocumentPDF:
                    case ResourceType.Text:
                    case ResourceType.WebsiteUrl:
                    case ResourceType.Json:
                        {
                            attachment = new HeroCard
                            {
                                Title = res.Title,
                                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, title: $"Open {Enum.GetName(res.Type.GetType(), res.Type)}", value: res.Location) }
                            }.ToAttachment();
                        }
                        break;
                    case ResourceType.Video:
                        {
                            var videoCard = new VideoCard(media: new[] { new MediaUrl(res.Location) });
                            videoCard.Title = res.Title;
                            attachment = videoCard.ToAttachment();
                        }
                        break;
                    default:
                        break;
                }

                return attachment;

            }).ToList();

            return activity;
        }

    }
}
