using System;
using System.Collections.Generic;
using System.Text;

namespace Philips.Chatbots.Database.MongoDB
{
    /// <summary>
    /// Database global constants.
    /// </summary>
    public static class DatabaseConstants
    {
        public const string DefaultBotName = "Philips.Chatbots.Bots.BotAlpha";
        public const string DefaultDatabaseName = "PhilipsChatbots";
        public const string LocalConnectionString = "mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb";
    }
}
