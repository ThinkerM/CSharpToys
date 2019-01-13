using System.Collections.Generic;

namespace DoubleLinkedQueue
{
    /// <summary>
    /// Double-ended queue. Suitable for pushing and popping elements from two opposite ends.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDeque<T> : IList<T>
    {
        void PushLast(T item);

        void PushFirst(T item);

        T PopLast();

        T PopFirst();

        T PeekLast();

        T PeekFirst();

        IDeque<T> Reverse();

        IEnumerator<T> GetReverseEnumerator();
    }
}
