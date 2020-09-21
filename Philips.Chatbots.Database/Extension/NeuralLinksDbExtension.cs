using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Database.Extension
{
    /// <summary>
    /// Neural link model databse extension class.
    /// </summary>
    public static class NeuralLinksDbExtension
    {
        /// <summary>
        /// Update neural lank with matching id.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateNeuralRankById<T>(this IMongoCollection<T> collection, string id, string nodeId) where T : NeuraLinkModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id && item.CildrenRank.Any(val => val.Key == nodeId),
                Builders<T>.Update.Inc(x => x.CildrenRank[-1].Value, 1));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set neural expression with matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static async Task<bool> SetNeuralExpById<T>(this IMongoCollection<T> collection, string id, INeuralExpression exp) where T : NeuraLinkModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id,
                Builders<T>.Update.Set(x => x.NeuralExp, exp));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set DecisionExpression forward action link with matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="actionLink"></param>
        /// <returns></returns>
        public static async Task<bool> SetNeuralExpForwardLinkById<T>(this IMongoCollection<T> collection, string id, ActionLink actionLink) where T : NeuraLinkModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id,
                Builders<T>.Update.Set(x => ((DecisionExpression)x.NeuralExp).ForwardAction, actionLink));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set DecisionExpression fallback action link with matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="actionLink"></param>
        /// <returns></returns>
        public static async Task<bool> SetNeuralExpFallbackLinkById<T>(this IMongoCollection<T> collection, string id, ActionLink actionLink) where T : NeuraLinkModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id,
                Builders<T>.Update.Set(x => ((DecisionExpression)x.NeuralExp).FallbackAction, actionLink));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Insert new childlink.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="parentId"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<T> InsertChildById<T>(this IMongoCollection<T> collection, string parentId, T val) where T : NeuraLinkModel
        {
            val._id = Guid.NewGuid().ToString();
            var res = await collection.AddChildLinkById(parentId, val._id);
            if (res)
                val.Parents.Add(parentId);
            await collection.InsertOneAsync(val);
            return val;
        }

        /// <summary>
        /// Add single message note with matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> AddNoteById<T>(this IMongoCollection<T> collection, string id, string val) where T : NeuraLinkModel
        {
            var res = await collection.UpdateOneAsync(_ => _._id == id,
               Builders<T>.Update.AddToSet(_ => _.Notes, val));
            return res.ModifiedCount > 0;
        }

        /// <summary>
        /// Remove single note with matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> RemoveNoteById<T>(this IMongoCollection<T> collection, string id, string val) where T : NeuraLinkModel
        {
            var res = await collection.UpdateOneAsync(_ => _._id == id,
               Builders<T>.Update.Pull(_ => _.Notes, val));
            return res.ModifiedCount > 0;
        }

        /// <summary>
        /// Add single label matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> AddLabelById<T>(this IMongoCollection<T> collection, string id, string val) where T : NeuraLinkModel
        {
            var res = await collection.UpdateOneAsync(_ => _._id == id,
               Builders<T>.Update.AddToSet(_ => _.Labels, val));
            return res.ModifiedCount > 0;
        }

        /// <summary>
        /// Remove single label matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> RemoveLabelById<T>(this IMongoCollection<T> collection, string id, string val) where T : NeuraLinkModel
        {
            var res = await collection.UpdateOneAsync(_ => _._id == id,
               Builders<T>.Update.Pull(_ => _.Labels, val));
            return res.ModifiedCount > 0;
        }

        /// <summary>
        /// Links two nodes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="parentId"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        public static async Task<bool> LinkParentChild<T>(this IMongoCollection<T> collection, string parentId, string childId) where T : NeuraLinkModel
        {
            return await collection.AddChildLinkById(parentId, childId) && await collection.AddParentLinkById(childId, parentId);
        }

        /// <summary>
        /// Unlinks two nodes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="parentId"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        public static async Task<bool> UnLinkParentChild<T>(this IMongoCollection<T> collection, string parentId, string childId) where T : NeuraLinkModel
        {
            return await collection.RemoveChildLinkById(parentId, childId) && await collection.RemoveParentLinkById(childId, parentId);
        }

        #region privateMethods
        private static async Task<bool> AddParentLinkById<T>(this IMongoCollection<T> collection, string childId, string parentId) where T : NeuraLinkModel
        {
            //Add parent link to child.
            var res = await collection.UpdateOneAsync(_ => _._id == childId,
                Builders<T>.Update.AddToSet(_ => _.Parents, childId));
            return res.ModifiedCount > 0;
        }

        private static async Task<bool> AddChildLinkById<T>(this IMongoCollection<T> collection, string parentId, string childId) where T : NeuraLinkModel
        {
            //Add child link to parent.
            var res = await collection.UpdateOneAsync(_ => _._id == parentId,
                Builders<T>.Update.AddToSet(_ => _.CildrenRank, new KeyValuePair<string, long>(childId, 0)));
            return res.ModifiedCount > 0;
        }

        private static async Task<bool> RemoveChildLinkById<T>(this IMongoCollection<T> collection, string parentId, string childId) where T : NeuraLinkModel
        {
            //Remove child link in parent.
            var result = await collection.UpdateOneAsync(item => item._id == parentId,
               Builders<T>.Update.PullFilter(x => x.CildrenRank, Builders<KeyValuePair<string, long>>.Filter.Eq(item => item.Key, childId)));
            return result.ModifiedCount > 0;
        }

        private static async Task<bool> RemoveParentLinkById<T>(this IMongoCollection<T> collection, string childId, string parentId) where T : NeuraLinkModel
        {
            //Remove parent link in child.
            var result = await collection.UpdateOneAsync(item => item._id == childId,
               Builders<T>.Update.Pull(x => x.Parents, parentId));
            return result.ModifiedCount > 0;
        }
        #endregion
    }
}
