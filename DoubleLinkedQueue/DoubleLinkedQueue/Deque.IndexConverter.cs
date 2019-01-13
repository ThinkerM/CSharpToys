namespace DoubleLinkedQueue
{
    public partial class Deque<T>
    {
        private class IndexConverter
        {
            private readonly Deque<T> deque;

            public IndexConverter(Deque<T> deque)
            {
                this.deque = deque;
            }

            private int FirstBlockUsed => deque.FirstDataBlockUsed;
            private int FirstBlockFirstPosition => deque.DataBlocks[FirstBlockUsed].FirstUsed;

            /// <summary>
            /// Find the index of the <see cref="DataBlock"/> which contains the specified <see cref="flatIndex"/>
            /// </summary>
            /// <param name="flatIndex">Index referred to from the outside (e.g. through array indexer calls)</param>
            /// <returns></returns>
            public int TargetBlockIndex(int flatIndex)
            {
                return FirstBlockUsed + (FirstBlockFirstPosition + flatIndex) / DataBlockSize;
            }

            /// <summary>
            /// Find the specific index inside a <see cref="DataBlock"/> where the position will end up in
            /// </summary>
            /// <param name="flatIndex">Index referred to from the outside (e.g. through array indexer calls)</param>
            /// <returns></returns>
            public int TargetItemIndexInBlock(int flatIndex)
            {
                return (FirstBlockFirstPosition + flatIndex) % DataBlockSize;
            }

            /// <summary>
            /// Calculate the index from a "coordinate" set of a <see cref="DataBlock"/> index and an index within that data block. 
            /// The returned value is equivalent to a value of e.g. an array indexer access from an outside call.
            /// </summary>
            /// <param name="blockIndex"></param>
            /// <param name="positionInBlock"></param>
            /// <returns></returns>
            public int FlatIndex(int blockIndex, int positionInBlock)
            {
                int blocksDifference = blockIndex - FirstBlockUsed;
                int positionDifference = positionInBlock - FirstBlockFirstPosition;
                return blocksDifference * DataBlockSize + positionDifference;
            }
        }
    }
}