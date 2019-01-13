namespace TextAlignment.Text.ReadingInfo
{
    /// <summary>
    /// Represents information about the process of reading from a filestream 
    /// (first encountered printable character, amount of line breaks encountered, information if end of file has been reached)
    /// </summary>
    public abstract class ReadingInfo<T>
    {
        protected T FirstEncounteredOccurence { get; }

        public int LineBreaksEncountered { get; }

        public bool EndOfFile { get; }

        protected ReadingInfo(
            T firstEncounteredOccurence = default(T),
            int lineBreaksEncountered = 0,
            bool endOfFile = false)
        {
            FirstEncounteredOccurence = firstEncounteredOccurence;
            LineBreaksEncountered = lineBreaksEncountered;
            EndOfFile = endOfFile;
        }

        public static implicit operator T(ReadingInfo<T> info)
            => info.FirstEncounteredOccurence;

        public override string ToString()
            => $"{FirstEncounteredOccurence} | Line breaks: {LineBreaksEncountered}";
    }
}
