using System.Threading.Tasks;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Database.Extension
{
    public static class NeuralActionsDbExtension
    {
        /// <summary>
        /// Maps 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public static async Task<bool> MapResourceById<T>(this IMongoCollection<T> collection, string nodeId, string resourceId) where T : NeuralActionModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == nodeId,
                Builders<T>.Update.AddToSet(x => x.Resources, resourceId));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="nodeId"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public static async Task<bool> UnmapResourceById<T>(this IMongoCollection<T> collection, string nodeId, string resourceId) where T : NeuralActionModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == nodeId,
                Builders<T>.Update.Pull(x => x.Resources, resourceId));
            return result.ModifiedCount > 0;
        }

    }
}
