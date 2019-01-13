using System;

namespace DoubleLinkedQueue
{
    public partial class Deque<T>
    {
        private class ReverseEnumerator : Enumerator
        {
            public ReverseEnumerator(Deque<T> deque) 
                : base(deque,
                      deque.LastDataBlockUsed,
                      deque.DataBlocks[deque.LastDataBlockUsed].LastUsed + 1)
            { }

            public override bool MoveNext()
            {
                if (!Valid) throw new InvalidOperationException();

                if (CurrentPosition == 0)
                {
                    CurrentBlock--;
                    CurrentPosition = DataBlockSize - 1;
                }
                else
                {
                    CurrentPosition--;
                }

                try
                {
                    return Deque.DataBlocks[CurrentBlock].IsUsed(CurrentPosition);
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }
            }
        }
    }
}