using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DoubleLinkedQueue
{
    public partial class Deque<T>
    {
        private class DataBlock : IDeque<T>
        {
            private readonly T[] data;
            private const int UNINITIALISED_INDEX = -1;

            public int Count
            {
                get
                {
                    return FirstUsed == UNINITIALISED_INDEX && LastUsed == UNINITIALISED_INDEX
                        ? 0
                        : LastUsed - FirstUsed + 1;
                }
            }

            public int LastUsed { get; private set; } = UNINITIALISED_INDEX;

            public int FirstUsed { get; private set; } = UNINITIALISED_INDEX;

            private void Uninitialise()
            {
                LastUsed = UNINITIALISED_INDEX;
                FirstUsed = UNINITIALISED_INDEX;
            }

            public static DataBlock InitialiseFromEnd(T initialItem)
            {
                return new DataBlock(DataBlockSize)
                {
                    LastUsed = DataBlockSize - 1,
                    FirstUsed = DataBlockSize - 1,
                    [DataBlockSize - 1] = initialItem
                };
            }

            public DataBlock() : this(DataBlockSize)
            { }

            private DataBlock(int size)
            {
                data = new T[size];
            }

            public T this[int index]
            {
                get
                {
                    if (!IsUsed(index)) throw new IndexOutOfRangeException();
                    return data[index];
                }
                set
                {
                    if (!IsUsed(index)) throw new IndexOutOfRangeException();
                    data[index] = value;
                }
            }

            private bool IsUsed(int index) => index.IsBetween(FirstUsed, LastUsed) && index != UNINITIALISED_INDEX;

            public bool CanPushLast() => LastUsed == UNINITIALISED_INDEX || LastUsed < DataBlockSize - 1;

            public bool CanPushFirst() => FirstUsed == UNINITIALISED_INDEX || FirstUsed > 0;

            public bool HasData() => FirstUsed != UNINITIALISED_INDEX || LastUsed != UNINITIALISED_INDEX;

            private int InsertionIndexLast()
            {
                if (LastUsed == UNINITIALISED_INDEX)
                    return 0;

                Debug.Assert(LastUsed + 1 < DataBlockSize);
                return LastUsed + 1;
            }

            private int InsertionIndexFirst()
            {
                if (FirstUsed == UNINITIALISED_INDEX)
                    return 0;

                Debug.Assert(FirstUsed -1 >= 0);
                return FirstUsed - 1;
            }

            public void PushLast(T item)
            {
                if (!CanPushLast()) throw new InvalidOperationException("Data block full, cannot add last.");

                int insertionIndex = InsertionIndexLast();
                data[insertionIndex] = item;
                LastUsed = insertionIndex;

                if (FirstUsed == UNINITIALISED_INDEX)
                    FirstUsed = insertionIndex;
            }

            public void PushFirst(T item)
            {
                if (!CanPushFirst()) throw new InvalidOperationException("Data block full, cannot add first.");

                int insertionIndex = InsertionIndexFirst();
                data[insertionIndex] = item;
                FirstUsed = insertionIndex;

                if (LastUsed == UNINITIALISED_INDEX)
                    LastUsed = insertionIndex;
            }

            public T PopLast()
            {
                if (!HasData()) throw new InvalidOperationException("No data to be popped.");

                T extractedItem = data[LastUsed];
                data[LastUsed] = default(T);
                if (LastUsed == 0 || LastUsed == FirstUsed)
                {
                    Uninitialise();
                }
                else
                {
                    LastUsed--;
                }
                return extractedItem;
            }

            public T PopFirst()
            {
                if (!HasData()) throw new InvalidOperationException("No data to be popped.");

                T extractedItem = data[FirstUsed];
                data[FirstUsed] = default(T);
                if (FirstUsed == DataBlockSize - 1 || FirstUsed == LastUsed)
                {
                    Uninitialise();
                }
                else
                {
                    FirstUsed++;
                }
                return extractedItem;
            }

            public T PeekLast()
            {
                if (!HasData()) throw new InvalidOperationException("No data");
                return data[LastUsed];
            }

            public T PeekFirst()
            {
                if (!HasData()) throw new InvalidOperationException("No data");
                return data[FirstUsed];
            }

            public IDeque<T> Reverse() => new ReverseDeque<T>(this);

            public override string ToString()
            {
                return $"DataBlock [{Count} elements | First used: {FirstUsed} | Last used: {LastUsed}]";
            }

            #region IEnumerable
            public IEnumerator<T> GetEnumerator()
            {
                for (int i = FirstUsed; i <= LastUsed && i >= 0; i++)
                {
                    yield return data[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IEnumerator<T> GetReverseEnumerator()
            {
                for (int i = LastUsed; i >= FirstUsed && i >= 0; i--)
                {
                    yield return data[i];
                }
            }
            #endregion

            #region ICollection
            void ICollection<T>.Add(T item) => PushLast(item);

            void ICollection<T>.Clear() => data.Initialize();

            public bool Contains(T item) => IndexOf(item) >= 0;

            void ICollection<T>.CopyTo(T[] array, int arrayIndex) => data.CopyTo(array, arrayIndex);

            public bool Remove(T item)
            {
                int itemIndex = IndexOf(item);
                bool canRemove = itemIndex >= 0 && itemIndex < data.Length;
                if (canRemove) RemoveAt(itemIndex);
                return canRemove;
            }

            bool ICollection<T>.IsReadOnly => false;
            #endregion

            #region IList
            public int IndexOf(T item)
            {
                for (int i = FirstUsed; i <= LastUsed && i >= 0; i++)
                {
                    if ((item != null && item.Equals(data[i])) 
                        || (item == null && data[i] == null))
                        return i;
                }
                return -1;
            }

            void IList<T>.Insert(int index, T item)
            {
                throw new NotSupportedException();
            }

            public void RemoveAt(int index)
            {
                data[index] = default(T);
            }
            #endregion
        }
    }
}