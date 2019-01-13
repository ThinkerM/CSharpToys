using System;
using System.Collections.Generic;
using TextAlignment.Text.ReadingInfo;

namespace TextAlignment.Contracts
{
    /// <summary>
    /// Provides methods to parse given input
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Seeks the first upcoming occurence of a printable character and returns it 
        /// or indicates that end of the input was reached instead
        /// </summary>
        /// <returns></returns>
        PrintableCharInfo NextPrintable();

        /// <summary>
        /// Seeks the first upcoming occurence of a sequence of printable characters and returns it
        /// or indicates that end of the input was reached instead
        /// </summary>
        /// <returns></returns>
        WordInfo NextWord();

        /// <summary>
        /// Extract all words (sequences of printable characters) from the input
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> ReadWords();

        /// <summary>
        /// Discard all characters until the first occurence of a printable one.
        /// Consumes the whole input if no more printables are contained in it.
        /// </summary>
        void SkipWhitespaces();

        /// <summary>
        /// Raised when the reader detects a pattern which corresponds to a new paragraph
        /// </summary>
        event EventHandler NewParagraphEncountered;
    }
}
