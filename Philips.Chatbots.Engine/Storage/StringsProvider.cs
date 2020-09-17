using Philips.Chatbots.Database.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using static Philips.Chatbots.Database.Common.DbAlias;

namespace Philips.Chatbots.Session
{
    /// <summary>
    /// String localization cache class.
    /// </summary>
    public static class StringsProvider
    {

        private static Lazy<Dictionary<string, string>> cache = new Lazy<Dictionary<string, string>>(
            () => DbBotCollection.GetFieldValue(DbAlias.BotAlphaName, item => item.Configuration.ResourceStrings)
                                .Result.ToDictionary(x => x.Key, y => y.Value));

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
