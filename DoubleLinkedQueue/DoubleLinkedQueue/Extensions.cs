using System;

namespace DoubleLinkedQueue
{
    internal static class Extensions
    {
        /// <summary>
        /// Determines if the item is within the boundaries of two other items (inclusive bounds)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T item, T left, T right)
            where T : IComparable<T>
        {
            return (item.CompareTo(left) >= 0 && item.CompareTo(right) <= 0)
                   ||
                   (item.CompareTo(left) <= 0 && item.CompareTo(right) >= 0);
        }

        /// <summary>
        /// Determines if the item is within the exlusive bounds of two other items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool IsBetweenExclusive<T>(this T item, T left, T right)
            where T : IComparable<T>
        {
            return (item.CompareTo(left) > 0 && item.CompareTo(right) < 0)
                   ||
                   (item.CompareTo(left) < 0 && item.CompareTo(right) > 0);
        }

        public static int IndexOf<T>(this T[] items, Predicate<T> searchCondition)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (searchCondition(items[i]))
                    return i;
            }
            return -1;
        }
    }
}
