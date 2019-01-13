using System.Collections.Generic;

namespace WordCountListing
{
    internal static class Extensions
    {
        public static bool IsPrintable(this char value)
            => (value != ' ' && value != '\t' && value != '\n');

        public static bool NotPrintable(this char value)
            => !value.IsPrintable();

        /// <summary>
        /// Increase the value for a given key by the specified amount (default 1).
        /// Extracted from https://github.com/ThinkerM/CSharp-Extensions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary">Dictionary to increment in</param>
        /// <param name="key">Key whose value will be incremented</param>
        /// <param name="amount"></param>
        public static void Increment<T>(this IDictionary<T, int> dictionary, T key, int amount = 1)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, 0);

            dictionary[key] += amount;
        }
    }
}