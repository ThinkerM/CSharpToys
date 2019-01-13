namespace DoubleLinkedQueue
{
    /// <summary>
    /// For purposes of evaluation environment only
    /// </summary>
    public static class DequeTest
    {
        //[System.Obsolete("Only for purposes of automated evaluation. Call IDeque.Reverse() directly.")]
        public static System.Collections.Generic.IList<T> GetReverseView<T>(Deque<T> d) => d.Reverse();
    }
}
