using System.Collections.Generic;

namespace CommonLibrary
{
    public static class CollectionsExtensions
    {
        public static IEnumerable<T> InPlaceReverse<T>(this T[] arrayToReverse)
        {
            for (int i = arrayToReverse.Length - 1; i >= 0; i--)
            {
                yield return arrayToReverse[i];
            }
        }

        public static IEnumerable<T> PopMultiple<T>(this Stack<T> stack, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return stack.Pop();
            }
        }
    }
}
