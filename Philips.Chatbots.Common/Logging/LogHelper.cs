using log4net;

namespace Philips.Chatbots.Common.Logging
{
    /// <summary>
    /// Logger extension methods.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Gets logger for the class type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILog GetLogger<T>() where T : class => LogManager.GetLogger(typeof(T));
    }
}
