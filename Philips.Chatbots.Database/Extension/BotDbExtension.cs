using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Database.Common;

namespace Philips.Chatbots.Database.Extension
{
    public static class BotDbExtension
    {
        /// <summary>
        /// Upsert bot configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<T> InsertNewOrUpdate<T>(this IMongoCollection<T> collection, T val) where T : BotModel
        {
            if (string.IsNullOrWhiteSpace(val._id))
                throw new ArgumentNullException(nameof(val._id));
            var res = await collection.ReplaceOneAsync(item => (item._id == val._id), val, new ReplaceOptions { IsUpsert = true });
            return val;
        }


        /// <summary>
        /// Get current active chat profile.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <returns></returns>
        public static async Task<BotChatProfile> GetActiveChatProfile<T>(this IMongoCollection<T> collection, string botId) where T : BotModel
        {
            var config = await collection.GetFieldValue(botId, item => item.Configuration);
            return config?.ChatProfiles?.FirstOrDefault(x => x.Name == config.ActiveProfile);
        }

        /// <summary>
        /// Get chat profile by botId.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static async Task<BotChatProfile> GetChatProfileById<T>(this IMongoCollection<T> collection, string botId, string profile) where T : BotModel
        {
            var config = await collection.GetFieldValue(botId, item => item.Configuration);
            return config?.ChatProfiles?.FirstOrDefault(x => x.Name == profile);
        }

        /// <summary>
        /// Remove chat profile.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static async Task<bool> RemoveChatProfileById<T>(this IMongoCollection<T> collection, string botId, string profile) where T : BotModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == botId,
            Builders<T>.Update.PullFilter<BotChatProfile>(x => x.Configuration.ChatProfiles, y => y.Name == profile));
            return result?.ModifiedCount > 0;
        }

        /// <summary>
        /// Add new chat profile or returns if matching profile name already exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static async Task<BotChatProfile> AddOrUpdateChatProfileById<T>(this IMongoCollection<T> collection, string botId, BotChatProfile profile) where T : BotModel
        {
            if (string.IsNullOrWhiteSpace(profile.Name))
                throw new ArgumentNullException(nameof(profile.Name));
            var res = await collection.GetChatProfileById(botId, profile.Name);
            if (res != null)
            {
                return res;
            }
            else
            {
                var result = await collection.UpdateOneAsync(item => item._id == botId, Builders<T>.Update.AddToSet(item => item.Configuration.ChatProfiles, profile));
                return result?.ModifiedCount > 0 ? profile : null;
            }
        }

        /// <summary>
        /// Update Root for specific chat profile
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="rootId"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static async Task<bool> SetRootNodeById<T>(this IMongoCollection<T> collection, string botId, string rootId, string profile = BotChatProfile.DefaultProfile) where T : BotModel
        {
            var node = await collection.GetChatProfileById(botId, profile);
            if (node == null)
            {
                node = new BotChatProfile { Name = profile };
                await collection.AddOrUpdateChatProfileById(botId, node);
            }
            var result = await collection.UpdateOneAsync(item => item._id == botId && item.Configuration.ChatProfiles.Any(val => val.Name == profile),
                Builders<T>.Update.Set(x => x.Configuration.ChatProfiles[-1].Root, rootId));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Add single string resource to the bot configuration, Make sure to not add duplicate keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> AddStringResourceById<T>(this IMongoCollection<T> collection, string botId, KeyValuePair<string, string> val) where T : BotModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == botId, Builders<T>.Update.AddToSet(item => item.Configuration.ResourceStrings, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Add batch string resources to the bot configuration, Make sure to not add duplicate keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public static async Task<bool> AddStringResourceBatchById<T>(this IMongoCollection<T> collection, string botId, List<KeyValuePair<string, string>> vals) where T : BotModel
        {
            UpdateResult result = null;
            foreach (var val in vals)
            {
                result = await collection.UpdateOneAsync(item => item._id == botId, Builders<T>.Update.AddToSet(item => item.Configuration.ResourceStrings, val));
            }
            return result?.ModifiedCount > 0;
        }

        /// <summary>
        /// Set active profile.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetActiveChatProfileById<T>(this IMongoCollection<T> collection, string botId, string val) where T : BotModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == botId, Builders<T>.Update.Set(item => item.Configuration.ActiveProfile, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Set Endpoint of the bot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetEndPointById<T>(this IMongoCollection<T> collection, string botId, string val) where T : BotModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == botId, Builders<T>.Update.Set(item => item.EndPoint, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        ///  Set description of the bot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static async Task<bool> SetDescriptionById<T>(this IMongoCollection<T> collection, string botId, string val) where T : BotModel
        {
            var result = await collection.UpdateOneAsync(item => item._id == botId, Builders<T>.Update.Set(item => item.Description, val));
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Gets single record matching _id from DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <returns></returns>
        public static async Task<T> FindOneById<T>(this IMongoCollection<T> collection, string botId) where T : BotModel
        {
            var result = await collection.FindAsync(item => item._id == botId);
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Deletes single record matching _id in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <returns></returns>
        public static async Task<bool> RemoveOneById<T>(this IMongoCollection<T> collection, string botId) where T : BotModel
        {
            var result = await collection.DeleteOneAsync(item => item._id == botId);
            return result.DeletedCount > 0;
        }

        /// <summary>
        /// Replaces single record matching _id in DB.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="botId"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static async Task<bool> ReplaceOneById<T>(this IMongoCollection<T> collection, string botId, T newValue) where T : BotModel
        {
            var result = await collection.ReplaceOneAsync(item => item._id == botId, newValue);
            return result.ModifiedCount > 0;
        }

    }
}
