using System.Collections.Generic;
using System.Text.Json;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Data.Models.Neural
{
    /// <summary>
    /// Neural link DB data model.
    /// </summary>
    public class NeuraLinkModel : ILinkInfo, INeuraLinkModel, IDataModel
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string _id { get; set; }

        [BsonIgnoreIfDefault]
        public bool IsArchived { get; set; }

        [BsonIgnoreIfDefault]
        public bool IsMarkedForDeletion { get; set; }

        [BsonIgnoreIfDefault]
        public string Name { get; set; }

        [BsonIgnoreIfDefault]
        public string Title { get; set; }

        [BsonIgnoreIfDefault]
        public string QuestionTitle { get; set; }

        [BsonIgnoreIfDefault]
        public string Description { get; set; }

        public List<string> Notes { get; set; } = new List<string>();

        public List<string> Parents { get; set; } = new List<string>();

        public List<KeyValuePair<string, long>> CildrenRank { get; set; } = new List<KeyValuePair<string, long>>();

        [BsonIgnoreIfDefault]
        public INeuralExpression NeuralExp { get; set; }

        public List<string> Labels { get; set; } = new List<string>();

        public string toString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
