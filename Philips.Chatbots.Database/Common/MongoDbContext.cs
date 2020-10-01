using MongoDB.Driver;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;
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
            _linkCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuralLinkModel) : $"{_activeProfile}_{nameof(NeuralLinkModel)}";
            _actionCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuralActionModel) : $"{_activeProfile}_{nameof(NeuralActionModel)}";
            _resourceCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuralResourceModel) : $"{_activeProfile}_{nameof(NeuralResourceModel)}";
            _trainDataCollectionName = _activeProfile == BotChatProfile.DefaultProfile ? nameof(NeuraTrainDataModel) : $"{_activeProfile}_{nameof(NeuraTrainDataModel)}";
        }

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
            IMongoDatabase db;

            if (_client == null)
            {
                db = MongoDbProvider.GetDatabase(dbName);
            }
            else
            {
                db = _client.GetDatabase(dbName);
            }
            await db.DropCollectionAsync(_linkCollectionName);
            await db.DropCollectionAsync(_actionCollectionName);
            await db.DropCollectionAsync(_resourceCollectionName);
            await db.DropCollectionAsync(_trainDataCollectionName);
        }


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
