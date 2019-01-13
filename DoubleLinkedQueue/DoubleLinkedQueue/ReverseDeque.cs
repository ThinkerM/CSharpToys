using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkedQueue
{
    internal class ReverseDeque<T> : IDeque<T>
    {
        private readonly IDeque<T> original;

        private int ReverseIndex(int originalIndex) => Count - originalIndex - 1;

        public ReverseDeque(IDeque<T> originalDeque)
        {
            original = originalDeque;
        }

        public void PushLast(T item) => original.PushFirst(item);

        public void PushFirst(T item) => original.PushLast(item);

        public T PopLast() => original.PopFirst();

        public T PopFirst() => original.PopLast();

        public T PeekLast() => original.PeekFirst();

        public T PeekFirst() => original.PeekLast();

        public IDeque<T> Reverse() => original;

        public IEnumerator<T> GetEnumerator() => original.GetReverseEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetReverseEnumerator() => original.GetEnumerator();

        public void Add(T item) => PushLast(item);

        public void Clear() => original.Clear();

        public bool Contains(T item) => original.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T value in this)
            {
                array[arrayIndex++] = value;
            }
        }

        public bool Remove(T item) => original.Remove(item);

        public int Count => original.Count;

        public bool IsReadOnly => original.IsReadOnly;

        public int IndexOf(T item)
        {
            int originalIndex = original.IndexOf(item);
            return ReverseIndex(originalIndex);
        }

        public void Insert(int index, T item)
        {
            original.Insert(ReverseIndex(index) + 1, item);
        }

        public void RemoveAt(int index)
        {
            original.RemoveAt(ReverseIndex(index));
        }

        public T this[int index]
        {
            get { return original[ReverseIndex(index)]; }
            set { original[ReverseIndex(index)] = value; }
        }
    }
}
