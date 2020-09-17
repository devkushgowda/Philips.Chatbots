using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Database.Extension
{
    public static class NeuralLinksDbExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateNeuralRankById<T>(this IMongoCollection<T> collection, string id, string nodeId) where T : NeuraLinkModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id && item.RankTable.Any(val => val.Key == nodeId),
                Builders<T>.Update.Inc(x => x.RankTable[-1].Value, 1));
            return result.ModifiedCount > 0;
        }

        public static async Task<bool> SetNeuralExpById<T>(this IMongoCollection<T> collection, string id, INeuralExpression exp) where T : NeuraLinkModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id,
                Builders<T>.Update.Set(x => x.NeuralExp, exp));
            return result.ModifiedCount > 0;
        }

        public static async Task<T> InsertChildById<T>(this IMongoCollection<T> collection, string parentId, T val) where T : NeuraLinkModel
        {
            val._id = Guid.NewGuid().ToString();
            var res = await collection.MapChildById(parentId, val._id);
            if (res)
                val.Parents.Add(parentId);
            await collection.InsertOneAsync(val);
            return val;
        }

        public static async Task<bool> AddNoteById<T>(this IMongoCollection<T> collection, string id, string val) where T : NeuraLinkModel
        {
            var res = await collection.UpdateOneAsync(_ => _._id == id,
               Builders<T>.Update.AddToSet(_ => _.Notes, val));
            return res.ModifiedCount > 0;
        }

        public static async Task<bool> DeleteNoteById<T>(this IMongoCollection<T> collection, string id, string val) where T : NeuraLinkModel
        {
            var res = await collection.UpdateOneAsync(_ => _._id == id,
               Builders<T>.Update.Pull(_ => _.Notes, val));
            return res.ModifiedCount > 0;
        }

        public static async Task<bool> AddLabelById<T>(this IMongoCollection<T> collection, string id, string val) where T : NeuraLinkModel
        {
            var res = await collection.UpdateOneAsync(_ => _._id == id,
               Builders<T>.Update.AddToSet(_ => _.Labels, val));
            return res.ModifiedCount > 0;
        }

        public static async Task<bool> DeleteLabelById<T>(this IMongoCollection<T> collection, string id, string val) where T : NeuraLinkModel
        {
            var res = await collection.UpdateOneAsync(_ => _._id == id,
               Builders<T>.Update.Pull(_ => _.Labels, val));
            return res.ModifiedCount > 0;
        }

        public static async Task<bool> MapChildById<T>(this IMongoCollection<T> collection, string parentId, string childId) where T : NeuraLinkModel
        {
            var res = await collection.UpdateOneAsync(_ => _._id == parentId,
                Builders<T>.Update.AddToSet(_ => _.Children, childId).AddToSet(_ => _.RankTable, new KeyValuePair<string, long>(childId, 0)));
            return res.ModifiedCount > 0;
        }
    }
}
