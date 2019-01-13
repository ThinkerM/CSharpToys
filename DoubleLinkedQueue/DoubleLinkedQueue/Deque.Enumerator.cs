using System;
using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkedQueue
{
    public partial class Deque<T>
    {
        private abstract class Enumerator : IEnumerator<T>
        {
            protected readonly Deque<T> Deque;
            private readonly int enumerationVersion;
            private readonly int initialDataBlock;
            private readonly int initialPosition;
            protected int CurrentBlock { get; set; }
            protected int CurrentPosition { get; set; }
            protected bool Valid => enumerationVersion == Deque.currentVersion;

            protected Enumerator(Deque<T> deque, int initialBlock, int initialPosition)
            {
                this.Deque = deque;
                enumerationVersion = deque.currentVersion;
                Deque.UpdateBlockMarkers();

                initialDataBlock = initialBlock;
                this.initialPosition = initialPosition;
                InitialisePositions();
            }

            private void InitialisePositions()
            {
                CurrentBlock = initialDataBlock;
                CurrentPosition = initialPosition;
            }

            public void Dispose() { }

            public abstract bool MoveNext();

            public void Reset() => InitialisePositions();

            public T Current
            {
                get
                {
                    if (!Valid) throw new InvalidOperationException();
                    try
                    {
                        return Deque.DataBlocks[CurrentBlock][CurrentPosition];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;
        }
    }
}