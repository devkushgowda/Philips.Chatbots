using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using System;
using System.Threading.Tasks;

namespace Philips.Chatbots.Database.MongoDB
{
    /// <summary>
    /// Database provider class
    /// </summary>
    public static class MongoDbProvider
    {
        private static bool load = true;

        static MongoDbProvider()
        {
            Connect();
        }

        // MongoClient is thread safe
        private static MongoClient dbClient;

        /// <summary>
        /// Need not to be threadsafe initilized only once from the main threads running Startup.cs,lobal.aspx etc.
        /// Ensure any option params are URL encoded.
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Connect(string connectionString = DatabaseConstants.LocalConnectionString)
        {
            RegisterClasses();

            if (string.IsNullOrWhiteSpace(connectionString))
                dbClient = new MongoClient();
            else
                dbClient = new MongoClient(connectionString ?? DatabaseConstants.LocalConnectionString);

        }

        /// <summary>
        /// When interface is stored in the database all its concrete classes are to be registered here.
        /// </summary>
        private static void RegisterClasses()
        {
            if (load)
            {
                BsonClassMap.RegisterClassMap<LinkExpression>();
                BsonClassMap.RegisterClassMap<DecisionExpression>();

                BsonClassMap.RegisterClassMap<ArithmeticOp>();
                BsonClassMap.RegisterClassMap<RelationalOp>();
                BsonClassMap.RegisterClassMap<ActionItem>();
                BsonClassMap.RegisterClassMap<ActionLink>();
                BsonClassMap.RegisterClassMap<ActionOption>();
                BsonClassMap.RegisterClassMap<LinkType>();
                BsonClassMap.RegisterClassMap<InnerExpEval>();
                load = false;
            }

        }

        /// <summary>
        /// Get or create the database of given name
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static IMongoDatabase GetDatabase(string dbName = DatabaseConstants.DefaultDatabaseName) => dbClient.GetDatabase(dbName);

        /// <summary>
        /// Get or create the collection of given name and type T, when collectionName is null then use name of T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static IMongoCollection<T> GetCollection<T>(string dbName = DatabaseConstants.DefaultDatabaseName, string collectionName = null) where T : IDataModel
        {
            collectionName = collectionName ?? typeof(T).Name;
            return GetDatabase(dbName)?.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Drop specified database.
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public async static Task DropDatabase(string dbName = DatabaseConstants.DefaultDatabaseName) => await dbClient.DropDatabaseAsync(dbName);

    }
}
