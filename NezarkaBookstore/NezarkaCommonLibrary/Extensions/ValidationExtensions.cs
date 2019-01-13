using System;
using System.ComponentModel;

namespace NezarkaCommonLibrary.Extensions
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Determines whether the number of passed arguments matches the <see cref="requiredAmount"/>
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="requiredAmount"></param>
        /// <returns></returns>
        public static bool ValidAmount(this string[] arguments, int requiredAmount)
            => arguments.Length == requiredAmount;

        /// <summary>
        /// Specify a condition which the argument must match
        /// </summary>
        /// <typeparam name="TArgument"></typeparam>
        /// <param name="argument"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public static bool CheckThat<TArgument>(this TArgument argument, Predicate<TArgument> check)
            => check(argument);

        /// <summary>
        /// Determines if a value in the array is convertible to the specified type
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="targetType"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool ValidTypeOnIndex(this string[] arguments, Type targetType, int index)
            => arguments[index].CanConvertTo(targetType);

        /// <summary>
        /// Determines whether the string can be converted to the specified type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool CanConvertTo(this string value, Type type)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            return converter.IsValid(value);
        }
    }
}