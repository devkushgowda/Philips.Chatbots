using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Data.Models.Neural
{
    /// <summary>
    /// Neural train data DB data model.
    /// </summary>
    public class NeuraTrainDataModel : INeuralTrainDataModel, IDataModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string _id { get; set; }

        [BsonIgnoreIfDefault]
        public bool IsArchived { get; set; }

        public List<string> Dataset { get; set; } = new List<string>();
    }
}
