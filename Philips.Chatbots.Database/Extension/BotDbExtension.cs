using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Database.Common;

namespace Philips.Chatbots.Database.Extension
{
    public static class BotDbExtension
    {
        /// <summary>
        /// Insert new bot configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="val"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<T> InsertNew<T>(this IMongoCollection<T> collection, T val, string id) where T : BotModel
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));
            val._id = id;
            var res = await collection.ReplaceOneAsync(item => (item._id == val._id), val, new ReplaceOptions { IsUpsert = true });
            return val;
        }

        /// <summary>
        /// Get root of the bot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<string> GetRootById<T>(this IMongoCollection<T> collection, string id) where T : BotModel
        {
            return await collection.GetFieldValue(id, item => item.Configuration.RootNode);
        }

        /// <summary>
        /// Set root of the bot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetRootNodeById<T>(this IMongoCollection<T> collection, string id, string val) where T : BotModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.Configuration.RootNode, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Add single string resource to the bot configuration, Make sure to not add duplicate keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> AddStringResourceById<T>(this IMongoCollection<T> collection, string id, KeyValuePair<string, string> val) where T : BotModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.AddToSet(item => item.Configuration.ResourceStrings, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Add batch string resources to the bot configuration, Make sure to not add duplicate keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public static async Task<bool> AddStringResourceBatchById<T>(this IMongoCollection<T> collection, string id, List<KeyValuePair<string, string>> vals) where T : BotModel
        {
            UpdateResult result = null;
            foreach (var val in vals)
            {
                result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.AddToSet(item => item.Configuration.ResourceStrings, val));
            }
            return result?.ModifiedCount > 0;
        }

        /// <summary>
        /// Set Endpoint of the bot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetEndPointById<T>(this IMongoCollection<T> collection, string id, string val) where T : BotModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.EndPoint, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set description of the bot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="type"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetDescriptionById<T>(this IMongoCollection<T> collection, string id, string val) where T : BotModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.Description, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Gets single record matching _id from DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> FindOneById<T>(this IMongoCollection<T> collection, string id) where T : BotModel
        {
            var result = await collection.FindAsync(item => item._id == id);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Deletes single record matching _id in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<bool> RemoveOneById<T>(this IMongoCollection<T> collection, string id) where T : BotModel
        {
            var result = await collection.DeleteOneAsync(item => item._id == id);
            return result.DeletedCount > 0;
        }

        ///// <summary>
        ///// Deletes all the records matching _id in DB.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="collection"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static async Task<long> RemoveManyById<T>(this IMongoCollection<T> collection, string id) where T : BotModel
        //{
        //    var result = await collection.DeleteManyAsync(item => item._id == id);
        //    return result.DeletedCount;
        //}

        /// <summary>
        /// Replaces single record matching _id in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static async Task<bool> ReplaceOneById<T>(this IMongoCollection<T> collection, string id, T newValue) where T : BotModel
        {
            var result = await collection.ReplaceOneAsync(item => item._id == id, newValue);
            return result.ModifiedCount > 0;
        }

    }
}
