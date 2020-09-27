using System;
using System.Collections.Generic;
using System.IO;
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
        /// Bot configuration.
        /// </summary>
        public BotConfiguration Configuration { get; set; } = new BotConfiguration();

    }

    /// <summary>
    /// Chat profiles model
    /// </summary>
    public class BotChatProfile
    {
        public const string DefaultProfile = nameof(DefaultProfile);
        public string Name { get; set; }
        public string Description { get; set; }
        public string Root { get; set; }
    }

    public class BotConfiguration
    {
        private string _currentProfile;
        private string _dataFolder = Path.Combine(Environment.CurrentDirectory, "data");

        /// <summary>
        /// Data folder where ML models are stored
        /// </summary>
        public string DataFolder
        {
            get => _dataFolder; set
            {
                _dataFolder = value;
            }
        }

        /// <summary>
        /// Currently used profile.
        /// </summary>
        public string ActiveProfile
        {
            get => _currentProfile ?? BotChatProfile.DefaultProfile; set
            {
                _currentProfile = value;
            }
        }

        /// <summary>
        /// Chat profiles.
        /// </summary>
        public List<BotChatProfile> ChatProfiles { get; set; } = new List<BotChatProfile>() { new BotChatProfile { Name = BotChatProfile.DefaultProfile } };

        /// <summary>
        /// Message/Errors template to be used by bot
        /// </summary>
        public List<KeyValuePair<string, string>> ResourceStrings { get; set; } = new List<KeyValuePair<string, string>>();

    }

}
