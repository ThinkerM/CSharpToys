using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelSort
{
    public class ParallelSort
    {
        private const int BlockSize = 128;

        public IList<T> Sort<T>(List<T> source) where T : IComparable<T> => Sort(source, Comparer<T>.Default);

        public IList<T> Sort<T>(List<T> source, IComparer<T> comparer) => Sort(source, comparer.Compare);

        public IList<T> Sort<T>(List<T> source, Comparison<T> comparison)
        {
            MergeBlocks(source, new List<T>(), comparison).ContinueWith((t) => SortBlock(t.Result, comparison));
        }

        private static Task<List<T>> MergeBlocks<T>(IList<T> sourceLeft, IList<T> sourceRight, Comparison<T> comparison)
        {
            return new Task<List<T>>(() =>
            {
                var merged = new List<T>(sourceLeft.Count + sourceRight.Count);
                int indexLeft = 0;
                int indexRight = 0;
                while (indexLeft < sourceLeft.Count && indexRight < sourceRight.Count)
                {
                    merged.Add(comparison(sourceLeft[indexLeft], sourceRight[indexRight]) <= 0
                                   ? sourceLeft[indexLeft++]
                                   : sourceRight[indexRight++]);
                }
                for (int i = indexLeft; i < sourceLeft.Count; i++)
                {
                    merged.Add(sourceLeft[i]);
                }
                for (int i = indexRight; i < sourceRight.Count; i++)
                {
                    merged.Add(sourceRight[i]);
                }
                return merged;
            });
        }

        private static Task SortBlock<T>(List<T> block, Comparison<T> comparison) => new Task(() => block.Sort(comparison));
    }
}
