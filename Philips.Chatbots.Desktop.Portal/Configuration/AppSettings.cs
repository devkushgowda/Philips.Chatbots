using Newtonsoft.Json;
using System;
using System.IO;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Philips.Chatbots.Desktop.Portal.Configuration
{
    public class AppSettings
    {
        /// <summary>
        /// Default configuration file
        /// </summary>
        public const string DefaultConfigurationFile = nameof(AppSettings) + ".json";
        public string MongoDbConnectionString { get; set; }

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
