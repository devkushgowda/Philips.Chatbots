using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Database.Extension
{
    /// <summary>
    /// CRUD extension for all the models implemented ILinkInfo interface
    /// </summary>
    public static class ILinkInfoDbExtension
    {
        /// <summary>
        /// Assigns new id and inserts record into DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<T> InsertNew<T>(this IMongoCollection<T> collection, T val) where T : ILinkInfo
        {
            val._id = Guid.NewGuid().ToString();
            await collection.InsertOneAsync(val);
            return val;
        }

        /// <summary>
        /// Gets single record matching _id from DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<T> FindOneById<T>(this IMongoCollection<T> collection, string id) where T : ILinkInfo
        {
            var result = await collection.FindAsync(item => item._id == id);
            return result.FirstOrDefault();
        }

        ///// <summary>
        ///// Gets all the records matching _id from DB.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="collection"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public static async Task<IEnumerable<T>> FindManyById<T>(this IMongoCollection<T> collection, string id) where T : ILinkInfo
        //{
        //    var result = await collection.FindAsync(item => item._id == id);
        //    return result.ToEnumerable();
        //}

        /// <summary>
        /// Deletes single record matching _id in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<bool> RemoveOneById<T>(this IMongoCollection<T> collection, string id) where T : ILinkInfo
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
        //public static async Task<long> DeleteManyById<T>(this IMongoCollection<T> collection, string id) where T : ILinkInfo
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
        public static async Task<bool> ReplaceOneById<T>(this IMongoCollection<T> collection, string id, T newValue) where T : ILinkInfo
        {
            var result = await collection.ReplaceOneAsync(item => item._id == id, newValue);
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set IsArchived by node id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetIsArchivedById<T>(this IMongoCollection<T> collection, string id, bool val) where T : ILinkInfo
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.IsArchived, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set IsMarkedForDeletion by node id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetMarkForDeletionById<T>(this IMongoCollection<T> collection, string id, bool val) where T : ILinkInfo
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.IsMarkedForDeletion, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set Name by node id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetNameById<T>(this IMongoCollection<T> collection, string id, string val) where T : ILinkInfo
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.Name, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set Title by node id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetTitleById<T>(this IMongoCollection<T> collection, string id, string val) where T : ILinkInfo
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.Title, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set QuestionTitle by node id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetQuestionTitleById<T>(this IMongoCollection<T> collection, string id, string val) where T : ILinkInfo
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.QuestionTitle, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set Description by node id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetDescriptionById<T>(this IMongoCollection<T> collection, string id, string val) where T : ILinkInfo
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.Description, val));
            return result.ModifiedCount > 0;
        }

    }
}
