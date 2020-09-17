using System.Collections.Generic;
using System.Text.Json;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Data.Models.Neural
{
    /// <summary>
    /// Neural resource DB data model.
    /// </summary>
    public class NeuralResourceModel : ILinkInfo, INeuralResourceModel, IDataModel
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

        public List<string> Labels { get; set; } = new List<string>();

        public ResourceType Type { get; set; } = ResourceType.Video;

        [BsonIgnoreIfDefault]
        public bool IsLocal { get; set; }

        [BsonIgnoreIfDefault]
        public string Location { get; set; }

        public string toString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
