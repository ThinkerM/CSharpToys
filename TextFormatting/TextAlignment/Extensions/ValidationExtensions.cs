using System;

namespace TextAlignment.Extensions
{
    public static class ValidationExtensions
    {
        public static bool ValidAmount(this string[] arguments, int requiredAmount)
            => arguments.Length == requiredAmount;

        public static bool MinimalAmount(this string[] arguments, int minimalAmount)
            => arguments.Length >= minimalAmount;

        public static void Validate<TArgument>(this TArgument argument, Predicate<TArgument> verification, Exception failException)
        {
            if (!verification(argument))
                throw failException;
        }

        public static bool ValidTypeOnIndex(this string[] arguments, Type targetType, int index)
            => arguments[index].CanConvertTo(targetType);
    }
}
