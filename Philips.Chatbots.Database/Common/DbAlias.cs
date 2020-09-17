using MongoDB.Driver;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.MongoDB;

namespace Philips.Chatbots.Database.Common
{
    /// <summary>
    /// Database collection alias.
    /// </summary>
    public static class DbAlias
    {
        public const string BotAlphaName = DatabaseConstants.DefaultBotName;
        public static IMongoCollection<NeuraLinkModel> DbLinkCollection => MongoDbProvider.GetCollection<NeuraLinkModel>();
        public static IMongoCollection<BotModel> DbBotCollection => MongoDbProvider.GetCollection<BotModel>();
        public static IMongoCollection<NeuralActionModel> DbActionCollection => MongoDbProvider.GetCollection<NeuralActionModel>();
        public static IMongoCollection<NeuralResourceModel> DbResourceCollection => MongoDbProvider.GetCollection<NeuralResourceModel>();
        public static IMongoCollection<NeuraTrainDataModel> DbTrainDataCollection => MongoDbProvider.GetCollection<NeuraTrainDataModel>();
    }
}
