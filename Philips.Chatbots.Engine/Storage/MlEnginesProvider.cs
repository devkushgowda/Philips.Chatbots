using System;
using System.Collections.Concurrent;

namespace Philips.Chatbots.Engine.Storage
{
    /// <summary>
    /// ML engine cache storage
    /// </summary>
    public static class MlEnginesProvider
    {

        private static ConcurrentDictionary<Type, object> cacheTrain = new ConcurrentDictionary<Type, object>();

        private static Lazy<ConcurrentDictionary<Type, object>> cachePredict = new Lazy<ConcurrentDictionary<Type, object>>(Initilize());

        private static ConcurrentDictionary<Type, object> Initilize()
        {
            var res = new ConcurrentDictionary<Type, object>();

            //fileSystemWatcher = new FileSystemWatcher
            //{
            //    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
            //    Filter = "*.zip",
            //    Path = Path.Combine(Environment.CurrentDirectory, "data")
            //};

            //fileSystemWatcher.Changed += FilesChanged;
            //fileSystemWatcher.Created += FilesChanged;

            return res;

        }

        //private static void FilesChanged(object sender, FileSystemEventArgs e)
        //{

        //}

        //private static FileSystemWatcher fileSystemWatcher;

        /// <summary>
        /// Get or create train engine instance and store in cache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetOrCreateTrainEngine<T>()
        {
            Type type = typeof(T);
            dynamic result;
            if (cacheTrain.ContainsKey(type))
            {
                cacheTrain.TryGetValue(type, out result);
            }
            else
            {
                result = Activator.CreateInstance(type);
                cacheTrain.TryAdd(type, result);
            }
            return (T)result;
        }

        /// <summary>
        /// Get or create predict engine instance and store in cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="init"></param>
        /// <returns></returns>
        public static T GetOrCreatePredictionEngine<T>(bool init = true)
        {
            Type type = typeof(T);
            dynamic result;
            if (cachePredict.Value.ContainsKey(type))
            {
                cachePredict.Value.TryGetValue(type, out result);
            }
            else
            {
                result = Activator.CreateInstance(type);
                if (init)
                    result.Initilize();
                cachePredict.Value.TryAdd(type, result);
            }
            return (T)result;
        }
    }
}
