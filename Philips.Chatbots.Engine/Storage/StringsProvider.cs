using Philips.Chatbots.Database.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using static Philips.Chatbots.Database.Common.DbAlias;
using System.Collections.Concurrent;

namespace Philips.Chatbots.Session
{
    /// <summary>
    /// String localization cache class.
    /// </summary>
    public static class StringsProvider
    {

        private static Lazy<ConcurrentDictionary<string, string>> cache = new Lazy<ConcurrentDictionary<string, string>>(
            () => LoadFromDB());

        private static ConcurrentDictionary<string, string> LoadFromDB()
        {
            var res = new ConcurrentDictionary<string, string>();
            DbBotCollection.GetFieldValue(DbAlias.BotAlphaName, item => item.Configuration.ResourceStrings)
                                .Result.ForEach(item => res.TryAdd(item.Key, item.Value));
            return res;
        }

        /// <summary>
        /// String localization.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string TryGet(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;
            return cache.Value.ContainsKey(key) ? cache.Value[key] : null;
        }

    }
}
