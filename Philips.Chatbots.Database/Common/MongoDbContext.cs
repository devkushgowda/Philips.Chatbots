using MongoDB.Bson;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Database.MongoDB;
using System;
using System.Threading.Tasks;

namespace Philips.Chatbots.Database.Common
{
    public class MongoDbContext
    {
        public const string BotAlphaName = DatabaseConstants.DefaultBotName;

        public const string LocalConnection = "Local";

        private IMongoClient _client;

        private string _activeProfile;

        private string _linkCollectionName;
        private string _actionCollectionName;
        private string _resourceCollectionName;
        private string _trainDataCollectionName;

        /// <summary>
        /// Create db context.
        /// </summary>
        /// <param name="profileName"></param>
        /// <param name="connectionString"></param>
        public MongoDbContext(string profileName = BotChatProfile.DefaultProfile, string connectionString = null)
        {
            if (connectionString != null)
            {
                _client = connectionString == LocalConnection ? new MongoClient() : new MongoClient(connectionString);
            }
            SyncChatProfile(profileName);
        }

        /// <summary>
        /// Sync collection names 
        /// </summary>
        public void SyncChatProfile(string profileName)
        {
            _activeProfile = profileName ?? throw new ArgumentNullException(nameof(profileName));
            _linkCollectionName = GetLinkCollectionName(_activeProfile);
            _actionCollectionName = GetActionCollectionName(_activeProfile);
            _resourceCollectionName = GetResourceCollectionName(_activeProfile);
            _trainDataCollectionName = GetTrainDataCollectionName(_activeProfile);
        }

        /// <summary>
        /// Check collection existance in database.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public async Task<bool> CollectionExistsAsync(IMongoDatabase db, string collectionName) => await (await db.ListCollectionsAsync(new ListCollectionsOptions { Filter = new BsonDocument("name", collectionName) })).AnyAsync();



        /// <summary>
        /// Rename existing chat profile.
        /// </summary>
        /// <param name="newProfileName"></param>
        /// <returns></returns>
        public async Task RenameCurrentChatProfile(string newProfileName)
        {
            await RenameChatProfile(_activeProfile, newProfileName);
            await BotCollection.SetActiveChatProfileById(BotAlphaName, newProfileName);
            SyncChatProfile(newProfileName);
        }

        /// <summary>
        /// Update old chat profile collections to new chat profile name. 
        /// </summary>
        /// <param name="oldProfileName"></param>
        /// <param name="newProfileName"></param>
        /// <returns></returns>
        public async Task RenameChatProfile(string oldProfileName, string newProfileName)
        {
            var db = GetDatabase();

            var profile = await BotCollection.GetActiveChatProfile(BotAlphaName);
            profile.Name = newProfileName;
            await BotCollection.AddOrUpdateChatProfileById(BotAlphaName, profile);
            await BotCollection.RemoveChatProfileById(BotAlphaName, _activeProfile);

            var curLinkCollectionName = GetLinkCollectionName(oldProfileName);
            if (await CollectionExistsAsync(db, curLinkCollectionName))
                await db.RenameCollectionAsync(curLinkCollectionName, GetLinkCollectionName(newProfileName));

            var curActionCollectionName = GetActionCollectionName(oldProfileName);
            if (await CollectionExistsAsync(db, curActionCollectionName))
                await db.RenameCollectionAsync(curActionCollectionName, GetActionCollectionName(newProfileName));

            var curResourceCollectionName = GetResourceCollectionName(oldProfileName);
            if (await CollectionExistsAsync(db, curResourceCollectionName))
                await db.RenameCollectionAsync(curResourceCollectionName, GetResourceCollectionName(newProfileName));

            var curTrainDataCollectionName = GetTrainDataCollectionName(oldProfileName);
            if (await CollectionExistsAsync(db, curTrainDataCollectionName))
                await db.RenameCollectionAsync(curTrainDataCollectionName, GetTrainDataCollectionName(newProfileName));

        }

        /// <summary>
        /// Get Link collection name based on given profile.
        /// </summary>
        /// <param name="profileName"></param>
        /// <returns></returns>
        public static string GetLinkCollectionName(string profileName) => profileName == BotChatProfile.DefaultProfile ? nameof(NeuralLinkModel) : $"{profileName}_{nameof(NeuralLinkModel)}";

        /// <summary>
        /// Get Action collection name based on given profile.
        /// </summary>
        /// <param name="profileName"></param>
        /// <returns></returns>
        public static string GetActionCollectionName(string profileName) => profileName == BotChatProfile.DefaultProfile ? nameof(NeuralActionModel) : $"{profileName}_{nameof(NeuralActionModel)}";

        /// <summary>
        /// Get Resource collection name based on given profile.
        /// </summary>
        /// <param name="profileName"></param>
        /// <returns></returns>
        public static string GetResourceCollectionName(string profileName) => profileName == BotChatProfile.DefaultProfile ? nameof(NeuralResourceModel) : $"{profileName}_{nameof(NeuralResourceModel)}";

        /// <summary>
        /// Get Train data collection name based on given profile.
        /// </summary>
        /// <param name="profileName"></param>
        /// <returns></returns>
        public static string GetTrainDataCollectionName(string profileName) => profileName == BotChatProfile.DefaultProfile ? nameof(NeuraTrainDataModel) : $"{profileName}_{nameof(NeuraTrainDataModel)}";

        /// <summary>
        /// Get collection of given database and collection name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(string dbName = DatabaseConstants.DefaultDatabaseName, string collectionName = null) where T : IDataModel
        {
            collectionName = collectionName ?? typeof(T).Name;
            var res = _client == null ? MongoDbProvider.GetCollection<T>(dbName, collectionName) : _client.GetDatabase(dbName).GetCollection<T>(collectionName);
            return res;
        }

        /// <summary>
        /// Drop databse.
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public async Task Drop(string dbName = DatabaseConstants.DefaultDatabaseName)
        {
            if (_client == null)
                await MongoDbProvider.DropDatabase(dbName);
            else
                await _client.DropDatabaseAsync(dbName);
        }

        /// <summary>
        /// Drop all data collections
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public async Task DropAllNodeCollections(string dbName = DatabaseConstants.DefaultDatabaseName)
        {
            IMongoDatabase db = GetDatabase(dbName);
            await db.DropCollectionAsync(_linkCollectionName);
            await db.DropCollectionAsync(_actionCollectionName);
            await db.DropCollectionAsync(_resourceCollectionName);
            await db.DropCollectionAsync(_trainDataCollectionName);
        }

        /// <summary>
        /// Get database by name.
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public IMongoDatabase GetDatabase(string dbName = DatabaseConstants.DefaultDatabaseName) => _client == null ? MongoDbProvider.GetDatabase(dbName) : _client.GetDatabase(dbName);


        /// <summary>
        /// Get bot configuration collection.
        /// </summary>
        public IMongoCollection<BotModel> BotCollection => GetCollection<BotModel>();

        /// <summary>
        /// Get neural link collection.
        /// </summary>
        public IMongoCollection<NeuralLinkModel> LinkCollection => GetCollection<NeuralLinkModel>(collectionName: _linkCollectionName);

        /// <summary>
        /// Get neural action collection.
        /// </summary>
        public IMongoCollection<NeuralActionModel> ActionCollection => GetCollection<NeuralActionModel>(collectionName: _actionCollectionName);

        /// <summary>
        /// Get neural resource collection.
        /// </summary>
        public IMongoCollection<NeuralResourceModel> ResourceCollection => GetCollection<NeuralResourceModel>(collectionName: _resourceCollectionName);

        /// <summary>
        /// Get neural train data collection.
        /// </summary>
        public IMongoCollection<NeuraTrainDataModel> TrainDataCollection => GetCollection<NeuraTrainDataModel>(collectionName: _trainDataCollectionName);

    }
}
