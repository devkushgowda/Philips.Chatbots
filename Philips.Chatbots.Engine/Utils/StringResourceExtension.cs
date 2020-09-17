using System.Linq;
using System.Text.RegularExpressions;
using Philips.Chatbots.Data.Models.Interfaces;

namespace Philips.Chatbots.Session
{
    /// <summary>
    /// String localization extension class to for dynamic formatting.
    /// </summary>
    public static class StringResourceExtension
    {
        private const int DbFormatLevel = 2;
        private const string RemoveSquareBraces = "(?<=\\[).+?(?=\\])";
        private const string FindSquareBraces = "\\[(.*?)\\]";
        private const string RemoveFlowerBraces = "(?<=\\{).+?(?=\\})";
        private const string FindFlowerBraces = "\\{(.*?)\\}";

        /// <summary>
        /// Format any given string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="link"></param>
        /// <returns></returns>
        public static string ApplyFormat<T>(this T link, string val, int dbLevel = DbFormatLevel) where T : IDataModel
        {
            while (--dbLevel > 0 && val.Contains("["))
                val = ApplyDbFormat(val);
            if (val.Contains("{"))
                val = link.ApplyPropertyFormat(val);
            return val;
        }

        /// <summary>
        /// Apply DB format.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ApplyDbFormat(string val)
        {
            var regex = new Regex(FindSquareBraces);
            var matches = regex.Matches(val).Select(it => it.Value).Distinct();

            foreach (var match in matches)
            {
                var curVal = StringsProvider.TryGet(Regex.Match(match, RemoveSquareBraces)?.Value ?? match);
                val = val.Replace(match, curVal);
            }
            return val;
        }

        /// <summary>
        /// Apply property format for all IlinkInfo and IDataModel objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="link"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ApplyPropertyFormat<T>(this T link, string val) where T : IDataModel
        {
            var regex = new Regex(FindFlowerBraces);
            var matches = regex.Matches(val).Select(it => it.Value).Distinct();

            foreach (var match in matches)
            {
                var curVal = GetPropertyValue(link, Regex.Match(match, RemoveFlowerBraces)?.Value);
                val = val.Replace(match, curVal);
            }
            return val;
        }

        /// <summary>
        /// Get object property value as string.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetPropertyValue(object obj, string propertyName)
        {
            try
            {
                return obj.GetType().GetProperty(propertyName).GetValue(obj, null).ToString();
            }
            catch
            {
                return null;
            }
        }

    }
}
