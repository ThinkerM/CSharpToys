using System;
using System.Collections.Generic;
using System.Text;
using TextAlignment.Contracts;
using TextAlignment.Extensions;
using TextAlignment.FileManipulation;
using TextAlignment.Text.ReadingInfo;

namespace TextAlignment.Text
{
    internal class Reader : IReader
    {
        public event EventHandler NewParagraphEncountered;

        private readonly IInputOutputWrapper io;

        public Reader(IInputOutputWrapper ioWrapper)
        {
            io = ioWrapper;
        }

        public PrintableCharInfo NextPrintable()
        {
            int lineBreaksFound = 0;
            while (io.HasInput)
            {
                char c = io.Read();
                if (c.IsPrintable())
                    return new PrintableCharInfo(c, lineBreaksFound);
                if (c.IsLineBreak())
                    lineBreaksFound++;
            }
            return new PrintableCharInfo(endOfFile: true);
        }

        public WordInfo NextWord()
        {
            PrintableCharInfo firstPrintable = NextPrintable();
            if (firstPrintable.EndOfFile)
                return new WordInfo(endOfFile: true);

            if (NewParagraph(firstPrintable))
            {
                NewParagraphEncountered?.Invoke(firstPrintable, EventArgs.Empty);
            }

            var word = new StringBuilder();
            word.Append(firstPrintable);

            while (io.HasInput && io.Peek().IsPrintable())
            {
                word.Append(io.Read());
            }

            return new WordInfo(word.ToString(), firstPrintable.LineBreaksEncountered);
        }

        public IEnumerable<string> ReadWords()
        {
            while (io.HasInput)
            {
                WordInfo word = NextWord();
                if (word.EndOfFile)
                    yield break;

                yield return word;
            }
        }

        public void SkipWhitespaces()
        {
            while (io.HasInput && io.Peek().NotPrintable())
            {
                io.Read();
            }
        }

        private static bool NewParagraph(int lineBreaksEncountered)
            => lineBreaksEncountered >= 2;

        private static bool NewParagraph<T>(ReadingInfo<T> info)
            => NewParagraph(info.LineBreaksEncountered);
    }
}
