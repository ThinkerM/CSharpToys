using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MergeSortQuery
{
    public class ParallelSort<T>
    {
        private readonly IComparer<T> comparer;

        public ParallelSort(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public void Sort(List<T> items, int threadsAvailable)
        {
            Sort(items, 0, items.Count, threadsAvailable);
        }

        private void Sort(List<T> items, int startIndex, int sortCount, int threads)
        {
            Debug.WriteLine($"\tThread {Thread.CurrentThread.ManagedThreadId}: start {startIndex} | count {sortCount} | {threads} {(threads == 1 ? "thread" : "threads")}");

            if (threads == 1)
            {
                Debug.WriteLine($"\t  {Thread.CurrentThread.ManagedThreadId}>> Sorting");
                items.Sort(startIndex, sortCount, comparer);
                return;
            }

            Debug.WriteLine($"\t  {Thread.CurrentThread.ManagedThreadId}>> Splitting");
            int threads1 = threads / 2;
            int threads2 = threads - threads1;
            int count1 = sortCount / 2;
            int count2 = sortCount - count1;
            int startIndex2 = startIndex + count1;

            var splitWork = new Thread(() => Sort(items, startIndex2, count2, threads2));
            splitWork.Start();
            Sort(items, startIndex, count1, threads1);
            splitWork.Join();

            var subsorted1 = items.GetRange(startIndex, count1);
            var subsorted2 = items.GetRange(startIndex2, count2);
            Merge(subsorted1, subsorted2, items, startIndex);
        }

        private void Merge(IList<T> source1, IList<T> source2, IList<T> mergeTarget, int targetStartIndex)
        {
            int i1 = 0;
            int i2 = 0;
            int currentMergeIndex = targetStartIndex;

            while (i1 < source1.Count && i2 < source2.Count)
            {
                bool sourcesComparison = comparer.Compare(source1[i1], source2[i2]) <= 0;
                mergeTarget[currentMergeIndex++] = sourcesComparison
                    ? source1[i1++]
                    : source2[i2++];
            }

            while (i1 < source1.Count) mergeTarget[currentMergeIndex++] = source1[i1++];
            while (i2 < source2.Count) mergeTarget[currentMergeIndex++] = source2[i2++];
        }
    }
}
