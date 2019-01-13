namespace TextAlignment.Text.ReadingInfo
{
    public class WordInfo : ReadingInfo<string>
    {
        public string Word
            => FirstEncounteredOccurence;

        public WordInfo(string word = default(string),
                        int lineBreaksEncountered = 0,
                        bool endOfFile = false)
            : base(word, lineBreaksEncountered, endOfFile)
        { }
    }
}
