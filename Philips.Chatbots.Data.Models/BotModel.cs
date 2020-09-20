using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Data.Models
{
    /// <summary>
    /// Bot data model.
    /// </summary>
    public class BotModel : IDataModel
    {
        /// <summary>
        /// Id/Name
        /// </summary>
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]

        public string _id { get; set; }

        /// <summary>
        /// Name of the bot
        /// </summary>
        [BsonIgnoreIfDefault]
        public string Description { get; set; }

        /// <summary>
        /// Api Endpoint
        /// </summary>
        [BsonIgnoreIfDefault]
        public string EndPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BotConfiguration Configuration { get; set; } = new BotConfiguration();

    }

    public class BotConfiguration
    {
        /// <summary>
        /// Neural start node
        /// </summary>
        [BsonIgnoreIfDefault]
        public string RootNode { get; set; }

        /// <summary>
        /// Message/Errors template to be used by bot
        /// </summary>
        public List<KeyValuePair<string, string>> ResourceStrings { get; set; } = new List<KeyValuePair<string, string>>();
    }

    /// <summary>
    /// Bot string globalization keys.
    /// </summary>
    public static class BotResourceKeyConstants
    {
        public const string Greeting = nameof(Greeting);
        public const string ThankYou = nameof(ThankYou);
        public const string Error = nameof(Error);
        public const string WhatIssue = nameof(WhatIssue); 
        public const string SelectedIssue = nameof(SelectedIssue);
        public const string WeHelpYou = nameof(WeHelpYou); 
        public const string FoundSolution = nameof(FoundSolution);
        public const string CannotMoveBack = nameof(CannotMoveBack);
        public const string InvalidInput = nameof(InvalidInput);
        public const string Feedback = nameof(Feedback);
        public const string ThankYouFeedback = nameof(ThankYouFeedback);
        public const string FeedBackOptions = nameof(FeedBackOptions);
        public const string StartAgain = nameof(StartAgain);
        public const string CommonActionOptions = nameof(CommonActionOptions); 
    }

}
