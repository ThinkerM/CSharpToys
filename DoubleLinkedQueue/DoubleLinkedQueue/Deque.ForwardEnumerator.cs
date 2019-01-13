using System;

namespace DoubleLinkedQueue
{
    public partial class Deque<T>
    {
        private class ForwardEnumerator : Enumerator
        {
            public ForwardEnumerator(Deque<T> deque)
                : base(deque,
                      deque.FirstDataBlockUsed,
                      deque.DataBlocks[deque.FirstDataBlockUsed].FirstUsed - 1)
            { }

            public override bool MoveNext()
            {
                if (!Valid) throw new InvalidOperationException();

                if (CurrentPosition >= DataBlockSize - 1)
                {
                    CurrentBlock++;
                    CurrentPosition = 0;
                }
                else
                {
                    CurrentPosition++;
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