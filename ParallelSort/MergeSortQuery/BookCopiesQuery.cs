using System;
using System.Collections.Generic;
using System.Linq;
using LibraryModel;

namespace MergeSortQuery
{
    internal class BookCopiesQuery
    {
        public int ThreadCount { get; set; }

        public Library Library { get; set; }

        private readonly Func<Copy, bool> filter;

        private readonly ParallelSort<Copy> parallelSort;

        public BookCopiesQuery()
        {
            parallelSort = new ParallelSort<Copy>(new DefaultCopyComparer());
            filter = copy => copy.State == CopyState.OnLoan
                       && copy.Book.Shelf[2] >= 'A' //shelves A-Q, letter is always on third position
                       && copy.Book.Shelf[2] <= 'Q';
        }

        public IList<Copy> ExecuteQuery()
        {
            if (ThreadCount == 0) throw new InvalidOperationException($"{nameof(ThreadCount)} property not set and default value 0 is not valid.");

            var filteredCopies = Library.Copies.Where(filter).ToList();
            parallelSort.Sort(filteredCopies, ThreadCount);
            return filteredCopies;
        }
    }
}
