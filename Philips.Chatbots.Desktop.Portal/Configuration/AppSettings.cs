using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Philips.Chatbots.Desktop.Portal.Configuration
{
    public class AppSettings
    {
        /// <summary>
        /// Default configuration file
        /// </summary>
        public const string DefaultConfigurationFile = nameof(AppSettings) + ".json";

        /// <summary>
        /// Default application password.
        /// </summary>
        private const string DefaultPassword = "password";

        private const string DefaultDatabase = "Local";

        /// <summary>
        /// Base64 password string
        /// </summary>
        public string Password { get; set; } = Convert.ToBase64String(Encoding.UTF8.GetBytes(DefaultPassword));

        public string ActiveDb { get; set; } = DefaultDatabase;

        public Dictionary<string, string> DbConnections { get; set; } = new Dictionary<string, string> { { DefaultDatabase, null } };

        /// <summary>
        /// Get password text.
        /// </summary>
        /// <returns></returns>
        public string GetPasswordText()
        {
            return Password == null ? DefaultPassword : Encoding.UTF8.GetString(Convert.FromBase64String(Password));
        }

        /// <summary>
        /// Gets active connection string
        /// </summary>
        /// <returns></returns>
        public string GetActiveDbConnectionString()
        {
            return (ActiveDb != null && (bool)DbConnections?.ContainsKey(ActiveDb)) ? DbConnections[ActiveDb] : DefaultDatabase;
        }

        /// <summary>
        /// Load configuration from file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="appendFolder"></param>
        /// <returns></returns>
        public static AppSettings LoadConfiguration(string fileName = DefaultConfigurationFile, bool appendFolder = true)
        {
            if (appendFolder)
                fileName = Path.Combine(Environment.CurrentDirectory, fileName);
            return JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(fileName));
        }

        /// <summary>
        /// Save configuration to file.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        /// <param name="appendFolder"></param>
        public static void SaveConfiguration(AppSettings obj, string fileName = DefaultConfigurationFile, bool appendFolder = true)
        {
            if (appendFolder)
                fileName = Path.Combine(Environment.CurrentDirectory, fileName);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(obj, Formatting.Indented));
        }
    }
}
