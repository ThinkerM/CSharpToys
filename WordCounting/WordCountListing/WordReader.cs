using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordCountListing
{
    internal class WordReader
    {
        private readonly StreamReader input;

        public WordReader(StreamReader input)
        {
            this.input = input;
        }
        
        /// <summary>
        /// Seeks the first occurence of a printable character or indicates that the end of input has been reached.
        /// <para/>Also counts the number of encountered line breaks during seeking the character.
        /// </summary>
        private char NextPrintable()
        {
            while (!input.EndOfStream)
            {
                char c = (char)input.Read();
                if (c.IsPrintable())
                    return c;
            }
            return default(char);
        }

        private string NextWord()
        {
            var word = new StringBuilder();
           char firstPrintable = NextPrintable();

            if (firstPrintable == default(char))
                return null;

            word.Append(firstPrintable);

            while (!input.EndOfStream && ((char)input.Peek()).IsPrintable())
            {
                word.Append((char)input.Read());
            }

            return word.ToString();
        }

        /// <summary>
        /// Extracts all words from the input and breaks once end of the input is reached.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetWords()
        {
            while (true)
            {
                string word = NextWord();
                if (string.IsNullOrEmpty(word))
                    yield break;

                yield return word;
            }
        }
    }
}
