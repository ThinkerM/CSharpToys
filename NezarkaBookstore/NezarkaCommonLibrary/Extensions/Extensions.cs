using System;
using System.Collections.Generic;

namespace NezarkaCommonLibrary.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Partition the source into batches of specified size (or smaller if not enough elements remain for a batch)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="batchSize">Number of elements to appear in each batch (less if not enough elements remain)</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(
            this IEnumerable<T> source, int batchSize)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    yield return YieldBatchElements(enumerator, batchSize - 1);
            }
        }

        private static IEnumerable<T> YieldBatchElements<T>(
            IEnumerator<T> source, int batchSize)
        {
            yield return source.Current;
            for (int i = 0; i < batchSize && source.MoveNext(); i++)
                yield return source.Current;
        }

        /// <summary>
        /// Change the input's first letter to upper-case
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Capitalized input</returns>
        public static string Capitalize(this string input) 
            => input[0].ToString().ToUpper() + input.Substring(1);

        /// <summary>
        /// Change the input's first letter to lower-case
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Capitalized input</returns>
        public static string Decapitalize(this string input) 
            => input[0].ToString().ToLower() + input.Substring(1);

        /// <summary>
        /// Indicates whether the specified string is null or a <see cref="string.Empty"/> string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
            => string.IsNullOrEmpty(value);

        /// <summary>
        /// Parse string into a specified enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum ParseEnum<TEnum>(this string value) 
            => value.ParseEnum<TEnum>(true);

        public static bool TryParseEnum<TEnum>(this string value, out TEnum result)
        {
            try
            {
                result = ParseEnum<TEnum>(value);
                return true;
            }
            catch (ArgumentException)
            {
                result = default(TEnum);
                return false;
            }
        }

        /// <summary>
        /// Parse string into a specified enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static TEnum ParseEnum<TEnum>(this string value, bool ignoreCase)
        {

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value = value.Trim();

            if (value.Length == 0)
            {
                throw new ArgumentException("Must specify valid information for parsing in the string.", nameof(value));
            }

            Type t = typeof(TEnum);

            if (!t.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.", nameof(TEnum));
            }

            return (TEnum)Enum.Parse(t, value, ignoreCase);
        }
    }
}
