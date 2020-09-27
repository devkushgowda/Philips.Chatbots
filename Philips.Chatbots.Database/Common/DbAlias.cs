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

        private static string _activeProfile = _botConfiguration?.Configuration?.ActiveProfile ?? BotChatProfile.DefaultProfile;

        private static string _linkCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuralLinkModel) : $"{_activeProfile}_{nameof(NeuralLinkModel)}";
        private static string _actionCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuralActionModel) : $"{_activeProfile}_{nameof(NeuralActionModel)}";
        private static string _resourceCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuralResourceModel) : $"{_activeProfile}_{nameof(NeuralResourceModel)}";
        private static string _trainDataCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuraTrainDataModel) : $"{_activeProfile}_{nameof(NeuraTrainDataModel)}";


        /// <summary>
        /// Get bot configuration collection.
        /// </summary>
        public static IMongoCollection<BotModel> DbBotCollection => MongoDbProvider.GetCollection<BotModel>();

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

            _activeProfile = profile?.Name ?? BotChatProfile.DefaultProfile;

            _linkCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuralLinkModel) : $"{_activeProfile}_{nameof(NeuralLinkModel)}";
            _actionCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuralActionModel) : $"{_activeProfile}_{nameof(NeuralActionModel)}";
            _resourceCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuralResourceModel) : $"{_activeProfile}_{nameof(NeuralResourceModel)}";
            _trainDataCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuraTrainDataModel) : $"{_activeProfile}_{nameof(NeuraTrainDataModel)}";

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
        public static IMongoCollection<NeuralLinkModel> DbLinkCollection => MongoDbProvider.GetCollection<NeuralLinkModel>(collectionName: _linkCollectionName);

        /// <summary>
        /// Get neural action collection.
        /// </summary>
        public static IMongoCollection<NeuralActionModel> DbActionCollection => MongoDbProvider.GetCollection<NeuralActionModel>(collectionName: _actionCollectionName);

        /// <summary>
        /// Get neural resource collection.
        /// </summary>
        public static IMongoCollection<NeuralResourceModel> DbResourceCollection => MongoDbProvider.GetCollection<NeuralResourceModel>(collectionName: _resourceCollectionName);

        /// <summary>
        /// Get neural train data collection.
        /// </summary>
        public static IMongoCollection<NeuraTrainDataModel> DbTrainDataCollection => MongoDbProvider.GetCollection<NeuraTrainDataModel>(collectionName: _trainDataCollectionName);
    }
}
