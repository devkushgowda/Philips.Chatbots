using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using System;
using System.Collections.Generic;
using System.Text;

namespace Philips.Chatbots.Engine.Request
{
    public static class AttachmentType
    {
        public const string Audio = "audio/mpeg3";
        public const string Video = "video/mp4";
        public const string ImagePNG = "image/png";
        public const string ImageGIF = "image/gif";
        public const string ImageJPG = "image/jpg";
        public const string DocumentPDF = "application/pdf";
        public const string Text = "text/plain";
        public const string Script = "text/script";
        public const string Json = "application/json";

        public static string GetAttachmentType(this NeuralResourceModel resource)
        {
            string result = null;

            switch (resource?.Type)
            {
                case ResourceType.DocumentPDF:
                    result = DocumentPDF;
                    break;
                case ResourceType.Text:
                    result = Text;
                    break;
                case ResourceType.ImagePNG:
                    result = ImagePNG;
                    break;
                case ResourceType.ImageJPG:
                    result = ImageJPG;
                    break;
                case ResourceType.ImageGIF:
                    result = ImageGIF;
                    break;
                case ResourceType.Video:
                    result = Video;
                    break;
                case ResourceType.Audio:
                    result = Audio;
                    break;
                case ResourceType.Script:
                    result = Script;
                    break;
                case ResourceType.Json:
                    result = Json;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
    public class AttachmentHelper
    {
    }
}
