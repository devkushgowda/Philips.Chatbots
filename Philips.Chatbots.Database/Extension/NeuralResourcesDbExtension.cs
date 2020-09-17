using System.Threading.Tasks;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Database.Extension
{
    public static class NeuralResourcesDbExtension
    {
        /// <summary>
        /// Set ResourceType for resource matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetResourceTypeById<T>(this IMongoCollection<T> collection, string id, ResourceType val) where T : NeuralResourceModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.Type, val));
            return result.ModifiedCount > 0;
        }
        /// <summary>
        /// Set IsLocal for resource matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetIsLocalById<T>(this IMongoCollection<T> collection, string id, bool val) where T : NeuralResourceModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.IsLocal, val));
            return result.ModifiedCount > 0;
        }
        /// <summary>
        /// Set Location for resource matching id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetLocationById<T>(this IMongoCollection<T> collection, string id, string val) where T : NeuralResourceModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == id, Builders<T>.Update.Set(item => item.Location, val));
            return result.ModifiedCount > 0;
        }
    }
}
