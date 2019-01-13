using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleWordCount
{
    internal class WordProcessor
    {
        private readonly StreamReader input;

        public WordProcessor(StreamReader input)
        {
            this.input = input;
        }

        /// <summary>
        /// Move the stream to the first occurence of a printable character
        /// (or consume the whole stream till the end if no more printables are encountered)
        /// </summary>
        /// <returns><see cref="char"/> value of the first encountered printable symbol, 
        /// null if end of stream was reached</returns>
        private char? FindNextPrintable()
        {
            while (!input.EndOfStream)
            {
                char c = (char)input.Read();
                if (c.IsPrintable())
                    return c;
            }
            return null;
        }

        /// <summary>
        /// Move beyond the next closest sequence of printable characters and indicate whether
        /// any such sequence was found or end of input was reached instead.
        /// </summary>
        /// <returns>True if at least one printable character to be discarded was found in the input.
        /// False if end of input was reached without encountering a printable character.</returns>
        private bool DiscardNextWord()
        {
           char? firstPrintable = FindNextPrintable();
           if (firstPrintable == null)
                return false;

            while (!input.EndOfStream && ((char)input.Peek()).IsPrintable())
            {
                input.Read();
            }
            return true;
        }

        public int CountWords()
        {
            int result = 0;
            while (!input.EndOfStream)
            {
                if (DiscardNextWord())
                    result++;
            }
            return result;
        }
    }
}
