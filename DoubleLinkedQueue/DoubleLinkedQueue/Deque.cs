using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace DoubleLinkedQueue
{
    public partial class Deque<T> : IDeque<T>
    {
        protected const int DataBlockSize = 256;

        private DataBlock[] DataBlocks { get; set; }

        public int Count { get; private set; }

        private readonly IndexConverter indexer;

        private int currentVersion = 0;

        #region Constructors
        public Deque() : this(initialCapacity: DataBlockSize * 3) //initialise with 3 data blocks by default, causing the middle one to be used for data insertions first
        { }

        public Deque(params T[] initialValues) : this(initialValues.Length)
        {
            FirstDataBlockUsed = 0; //override usage markers to 0, will be pushing from the very beginning (not the middle)
            LastDataBlockUsed = 0;
            foreach (var value in initialValues)
            {
                PushLast(value);
            }
        }

        public Deque(int initialCapacity)
        {
            int dataBlocksRequired = (int)Math.Ceiling((double)initialCapacity / DataBlockSize);
            DataBlocks = new DataBlock[dataBlocksRequired];
            for (int i = 0; i < DataBlocks.Length; i++)
            {
                DataBlocks[i] = new DataBlock();
            }
            int middleBlockIndex = DataBlocks.Length / 2;
            FirstDataBlockUsed = middleBlockIndex;
            LastDataBlockUsed = middleBlockIndex;

            this.indexer = new IndexConverter(this);
        }
        #endregion

        public void PushLast(T item)
        {
            if (!DataBlocks[LastDataBlockUsed].CanPushLast())
            {
                if (LastDataBlockUsed >= DataBlocks.Length - 1)
                {
                    AllocateNewBlockLast();
                }
                LastDataBlockUsed++;
            }
            DataBlocks[LastDataBlockUsed].PushLast(item);
            Count++;
            DataModified(CollectionChangeAction.Add, item);
            UpdateBlockMarkers();
        }

        public void PushFirst(T item)
        {
            if (DataBlocks[FirstDataBlockUsed].CanPushFirst())
            {
                DataBlocks[FirstDataBlockUsed].PushFirst(item);
            }
            else
            {
                if (FirstDataBlockUsed == 0)
                {
                    AllocateNewBlockFirst();
                    if (LastDataBlockUsed == 0)
                    {
                        LastDataBlockUsed = 1; //first and last were both 0, 1st should now be 0 and last 1
                    }
                }
                FirstDataBlockUsed--;
                DataBlocks[FirstDataBlockUsed] = DataBlock.InitialiseFromEnd(item);
            }
            Count++;
            DataModified(CollectionChangeAction.Add, item);
            UpdateBlockMarkers();
        }

        public T PopLast()
        {
            T popped = DataBlocks[LastDataBlockUsed].PopLast();
            Count--;
            DataModified(CollectionChangeAction.Remove, popped);
            UpdateBlockMarkers();
            return popped;
        }

        public T PopFirst()
        {
            T popped = DataBlocks[FirstDataBlockUsed].PopFirst();
            Count--;
            DataModified(CollectionChangeAction.Remove, popped);
            UpdateBlockMarkers();
            return popped;
        }

        public T PeekLast()
        {
            UpdateLastDataBlock();
            return DataBlocks[LastDataBlockUsed].PeekLast();
        }

        public T PeekFirst()
        {
            UpdateFirstDataBlock();
            return DataBlocks[FirstDataBlockUsed].PeekFirst();
        }

        public T this[int index]
        {
            get
            {
                int targetBlockIndex = indexer.TargetBlockIndex(index);
                int targetPosition = indexer.TargetItemIndexInBlock(index);
                return DataBlocks[targetBlockIndex][targetPosition];
            }
            set
            {
                int targetBlockIndex = indexer.TargetBlockIndex(index);
                int targetPosition = indexer.TargetItemIndexInBlock(index);

                DataBlocks[targetBlockIndex][targetPosition] = value;
                DataModified(CollectionChangeAction.Refresh, value);
            }
        }

        private int LastDataBlockUsed { get; set; }
        private void UpdateLastDataBlock()
        {
            if (Count == 0) return; //no data at all, nothing to update
            if (!DataBlocks[LastDataBlockUsed].HasData())
            {
                LastDataBlockUsed = NormalizeDataBlockIndex(LastDataBlockUsed - 1);
            }
        }

        private int FirstDataBlockUsed { get; set; }
        private void UpdateFirstDataBlock()
        {
            if (Count == 0) return; //no data at all, nothing to update
            FirstDataBlockUsed = NormalizeDataBlockIndex(FirstDataBlockUsed);
            if (!DataBlocks[FirstDataBlockUsed].HasData())
            {
                FirstDataBlockUsed = NormalizeDataBlockIndex(FirstDataBlockUsed + 1);
            }
        }

        private void UpdateBlockMarkers()
        {
            UpdateFirstDataBlock();
            UpdateLastDataBlock();
        }

        private void AllocateNewBlockFirst()
        {
            var newBlocks = new DataBlock[DataBlocks.Length + 1];
            newBlocks[0] = new DataBlock();
            for (int newBlocksIndex = 1, oldBlocksIndex = 0;
                newBlocksIndex < newBlocks.Length;
                newBlocksIndex++, oldBlocksIndex++)
            {
                newBlocks[newBlocksIndex] = DataBlocks[oldBlocksIndex];
            }
            DataBlocks = newBlocks;
            FirstDataBlockUsed++; //offset used marker, blocks were moved forward
            LastDataBlockUsed++;
        }

        private void AllocateNewBlockLast()
        {
            var newBlocks = new DataBlock[DataBlocks.Length + 1];
            for (int i = 0; i < DataBlocks.Length; i++)
            {
                newBlocks[i] = DataBlocks[i];
            }
            newBlocks[newBlocks.Length - 1] = new DataBlock();
            DataBlocks = newBlocks;
        }

        /// <summary>
        /// Check whether the given index is within range of the available datablocks, return the closest valid value if out of range
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int NormalizeDataBlockIndex(int index)
        {
            if (index < 0)
                return 0;
            if (index >= DataBlocks.Length)
                return DataBlocks.Length - 1;

            return index;
        }

        #region Shifting
        /// <summary>
        /// Apply a left-shift operation on all elements to the RIGHT from the specified index and beyond
        /// </summary>
        /// <param name="firstIndexToShift">Left-most element index which will be moved to the left</param>
        /// <param name="lastIndexToShift">Right-most element index to be shifted</param>
        /// <remarks>There is some inefficiency with all of the flat index conversions for every shift. Performing shifts within each block and then 
        /// specially treating overflow elements and first/last block should be more efficient, 
        /// but the way I wrote it was quite messy, so I decided to use this "worse but better to read" approeach instead.</remarks>
        private void ShiftRangeLeft(int firstIndexToShift, int lastIndexToShift)
        {
            for (int i = firstIndexToShift; i <= lastIndexToShift; i++)
            {
                ShiftLeft(i);
            }
        }

        /// <summary>
        /// Apply a right-shift operation on all elements to the LEFT of the specified index and before it
        /// </summary>
        /// <param name="firstIndexToShift">Left-most element index to be shifted</param>
        /// <param name="lastIndexToShift">Right-most element index which will be moved to the right</param>
        private void ShiftRangeRight(int firstIndexToShift, int lastIndexToShift)
        {
            for (int i = lastIndexToShift; i >= firstIndexToShift; i--)
            {
                ShiftRight(i);
            }
        }

        private void ShiftLeft(int flatIndex)
        {
            if (flatIndex == 0) throw new InvalidOperationException("Cannot shift the very first element.");
            this[flatIndex - 1] = this[flatIndex];
        }

        private void ShiftRight(int flatIndex)
        {
            if (flatIndex == Count - 1) throw new InvalidOperationException("Cannot shift the very last element.");
            this[flatIndex + 1] = this[flatIndex];
        }
        #endregion

        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            int enumerationVersion = currentVersion;
            for (int i = FirstDataBlockUsed; i <= LastDataBlockUsed; i++)
            {
                foreach (T item in DataBlocks[i])
                {
                    if (enumerationVersion != currentVersion)
                        throw new InvalidOperationException("Data was modified during enumeration");

                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IDeque<T> Reverse() => new ReverseDeque<T>(this);

        public IEnumerator<T> GetReverseEnumerator()
        {
            int enumerationVersion = currentVersion;
            for (int i = LastDataBlockUsed; i >= FirstDataBlockUsed; i--)
            {
                using (var blockReverseEnumerator = DataBlocks[i].GetReverseEnumerator())
                {
                    while (blockReverseEnumerator.MoveNext())
                    {
                        if (enumerationVersion != currentVersion)
                            throw new InvalidOperationException("Data was modified during enumeration");

                        yield return blockReverseEnumerator.Current;
                    }
                }
            }
        }

        private void DataModified(CollectionChangeAction action, T changedItem)
        {
            currentVersion++;
        }
        #endregion

        #region ICollection
        public void Add(T item) => PushLast(item);

        public void Clear()
        {
            for (int i = 0; i < DataBlocks.Length; i++)
            {
                DataBlocks[i] = new DataBlock();
            }
            Count = 0;
            FirstDataBlockUsed = DataBlocks.Length / 2;
            LastDataBlockUsed = DataBlocks.Length / 2;
            DataModified(CollectionChangeAction.Remove, default(T));
        }

        public bool Contains(T item)
        {
            return DataBlocks.SelectMany(dataBlock => dataBlock).Any(x => x.Equals(item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (T value in this)
            {
                array[arrayIndex++] = value;
            }
        }

        public bool Remove(T item)
        {
            int itemIndex = IndexOf(item);
            if (itemIndex == -1) return false;

            RemoveAt(itemIndex);
            DataModified(CollectionChangeAction.Remove, item);
            return true;
        }

        bool ICollection<T>.IsReadOnly => false;
        #endregion

        #region IList
        public int IndexOf(T item)
        {
            int containingBlockIndex = DataBlocks.IndexOf(block => block.Contains(item));
            if (containingBlockIndex == -1) return -1;

            int position = DataBlocks[containingBlockIndex].IndexOf(item);
            Debug.Assert(position != -1);
            return indexer.FlatIndex(containingBlockIndex, position);
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException();
            //check for special cases (very first/last element)
            if (index == Count)
            {
                PushLast(item);
                return;
            }
            if (index == 0)
            {
                PushFirst(item);
                return;
            }

            if (IsInSecondHalf(index))
            {
                PushLast(default(T)); //make space for an additional element at the end
                ShiftRangeRight(index, Count - 2);
            }
            else
            {
                PushFirst(default(T)); //make space for an additional element at the beginning
                ShiftRangeLeft(1, index);
            }
            this[index] = item;
            DataModified(CollectionChangeAction.Add, item);
        }

        public void RemoveAt(int index)
        {
            T removeddItem = this[index];
            if (IsInSecondHalf(index))
            {
                ShiftRangeLeft(index + 1, Count - 1);
                PopLast();
            }
            else
            {
                ShiftRangeRight(0, index - 1);
                PopFirst();
            }
            DataModified(CollectionChangeAction.Remove, removeddItem);
        }

        private bool IsInSecondHalf(int flatIndex) => flatIndex >= Count / 2;
        #endregion
    }
}