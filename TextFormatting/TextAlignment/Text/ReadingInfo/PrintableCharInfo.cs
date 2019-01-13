namespace TextAlignment.Text.ReadingInfo
{
    public class PrintableCharInfo : ReadingInfo<char>
    {
        public char PrintableChar
            => FirstEncounteredOccurence;

        public PrintableCharInfo(char firstEncountered = default(char),
                                 int lineBreaksEncountered = 0,
                                 bool endOfFile = false)
            : base(firstEncountered, lineBreaksEncountered, endOfFile)
        { }
    }
}
