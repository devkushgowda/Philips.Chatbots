using MongoDB.Driver;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Database.MongoDB;
using System.Threading.Tasks;

namespace Philips.Chatbots.Database.Common
{
    /// <summary>
    /// Database collection quick alias.
    /// </summary>
    public static class DbAlias
    {

        private static BotModel _botConfiguration = MongoDbProvider.GetCollection<BotModel>().Find(x => x._id == BotAlphaName).FirstOrDefault();

        private static MongoDbContext _dbContext = new MongoDbContext(_botConfiguration?.Configuration?.ActiveProfile ?? BotChatProfile.DefaultProfile);
        /// <summary>
        /// Get bot configuration collection.
        /// </summary>
        public static IMongoCollection<BotModel> DbBotCollection => MongoDbProvider.GetCollection<BotModel>();

        /// <summary>
        /// Get Mongo DB context object.
        /// </summary>
        public static MongoDbContext DbContext => _dbContext;

        /// <summary>
        /// Default bot name.
        /// </summary>
        public const string BotAlphaName = DatabaseConstants.DefaultBotName;

        /// <summary>
        /// Sync chat profile from db.
        /// Invoke this method in thread-safe manner.
        /// </summary>
        /// <returns></returns>
        public async static Task SyncChatProfile()
        {
            BotChatProfile profile = await CurrentChatProfile();
            if (profile == null)
                profile = await DbBotCollection.AddOrUpdateChatProfileById(BotAlphaName, new BotChatProfile() { Name = BotChatProfile.DefaultProfile });

            _dbContext.SyncChatProfile(profile?.Name ?? BotChatProfile.DefaultProfile);
        }

        /// <summary>
        /// Get active chat profile.
        /// </summary>
        /// <returns></returns>
        public static async Task<BotChatProfile> CurrentChatProfile() => await DbBotCollection.GetActiveChatProfile(BotAlphaName);

        /// <summary>
        /// Get bot configuration.
        /// </summary>
        /// <returns></returns>
        public static async Task<BotModel> BotConfiguration() => await DbBotCollection.FindOneById(BotAlphaName);

        /// <summary>
        /// Get neural link collection.
        /// </summary>
        public static IMongoCollection<NeuralLinkModel> DbLinkCollection => _dbContext.LinkCollection;

        /// <summary>
        /// Get neural action collection.
        /// </summary>
        public static IMongoCollection<NeuralActionModel> DbActionCollection => _dbContext.ActionCollection;

        /// <summary>
        /// Get neural resource collection.
        /// </summary>
        public static IMongoCollection<NeuralResourceModel> DbResourceCollection => _dbContext.ResourceCollection;

        /// <summary>
        /// Get neural train data collection.
        /// </summary>
        public static IMongoCollection<NeuraTrainDataModel> DbTrainDataCollection => _dbContext.TrainDataCollection;
    }
}
