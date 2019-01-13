using System;
using System.Collections.Generic;
using LibraryModel;

namespace MergeSortQuery
{
    internal class DefaultCopyComparer : IComparer<Copy>
    {
        public int Compare(Copy x, Copy y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            int dueDate = Comparison(c => c.OnLoan.DueDate);
            if (dueDate != 0) return dueDate;

            int lastName = Comparison(c => c.OnLoan.Client.LastName);
            if (lastName != 0) return lastName;

            int firstName = Comparison(c => c.OnLoan.Client.FirstName);
            if (firstName != 0) return firstName;

            int shelf = Comparison(c => c.Book.Shelf);
            if (shelf != 0) return shelf;

            return Comparison(c => c.Id);

            int Comparison<T>(Func<Copy,T> comparedProperty)
                where T : IComparable<T>
            {
                return comparedProperty(x).CompareTo(comparedProperty(y));
            }
        }
    }
}
