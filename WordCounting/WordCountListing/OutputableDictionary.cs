using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WordCountListing
{
    internal enum OutputMode { ByKey, ByValue }

    /// <summary>
    /// Extends <see cref="Dictionary{TKey,TValue}"/> to allow for simplified output
    /// ordered either by contained keys or values.
    /// </summary>
    /// <typeparam name="TKey">Type used as keys</typeparam>
    /// <typeparam name="TValue">Type used as values</typeparam>
    internal class OutputableDictionary<TKey,TValue> : Dictionary<TKey,TValue>
    {
        public void Write(OutputMode mode = OutputMode.ByKey)
        {
            switch (mode)
            {
                case OutputMode.ByKey:
                    OutputKeyValuePairs(this.OrderBy(x => x.Key));
                    break;
                case OutputMode.ByValue:
                    OutputKeyValuePairs(this.OrderByDescending(x => x.Value));
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Unexpected value of OutputMode: {mode}");
            }
        }

        private static void OutputKeyValuePairs(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            foreach (var keyValuePair in keyValuePairs)
            {
                Console.WriteLine($"{keyValuePair.Key}: {keyValuePair.Value}");
            }
        }
    }
}