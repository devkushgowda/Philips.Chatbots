using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Database.Extension
{
    public static class NeuralTrainDataDbExtension
    {
        /// <summary>
        /// Insert new record into DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<T> InsertNew<T>(this IMongoCollection<T> collection, T val) where T : INeuralTrainDataModel
        {
            if (string.IsNullOrWhiteSpace(val._id))
                throw new ArgumentNullException(nameof(val._id));
            await collection.InsertOneAsync(val);
            return val;
        }

        /// <summary>
        /// Add new training data to dataset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static async Task<bool> AddTrainData<T>(this IMongoCollection<T> collection, string id, string trainData) where T : NeuraTrainDataModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id,
            Builders<T>.Update.AddToSet(x => x.Dataset, trainData));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Delete matching train data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static async Task<bool> RemoveTrainData<T>(this IMongoCollection<T> collection, string id, string trainData) where T : NeuraTrainDataModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id,
                Builders<T>.Update.Pull(x => x.Dataset, trainData));
            return result.ModifiedCount > 0;
        }


        /// <summary>
        /// Gets single record matching _id from DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> FindOneById<T>(this IMongoCollection<T> collection, string id) where T : INeuralTrainDataModel
        {
            var result = await collection.FindAsync(item => item._id == id);
            return result.ToEnumerable();
        }

        /// <summary>
        /// Deletes single record matching _id in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<bool> RemoveOneById<T>(this IMongoCollection<T> collection, string id) where T : INeuralTrainDataModel
        {
            var result = await collection.DeleteOneAsync(item => item._id == id);
            return result.DeletedCount > 0;
        }

        /// <summary>
        /// Replaces single record matching _id in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static async Task<bool> ReplaceOneById<T>(this IMongoCollection<T> collection, string id, T newValue) where T : INeuralTrainDataModel
        {
            var result = await collection.ReplaceOneAsync(item => item._id == id, newValue);
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Archives the record matching _id in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetIsArchivedById<T>(this IMongoCollection<T> collection, string id, bool val) where T : INeuralTrainDataModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.IsArchived, val));
            return result.ModifiedCount > 0;
        }

    }
}
