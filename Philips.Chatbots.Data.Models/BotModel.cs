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

}
