using System.Collections.Generic;
using TextAlignment.Enums;

namespace TextAlignment.Contracts
{
    /// <summary>
    /// Provides methods to judge the properties of words and sets of words in relation to larger text structures
    /// </summary>
    public interface ITextEvaluator
    {
        /// <summary>
        /// Determines whether a line can receive another
        ///  word without exceeding the <see cref="requiredLineLength"/>
        /// </summary>
        /// <param name="word">String to be considered as a potential candidate for the line</param>
        /// <param name="wordsOnLine">Strings already contained within the line (i.e. without the <see cref="word"/> itself)</param>
        /// <param name="requiredLineLength"></param>
        /// <returns></returns>
        /// <remarks>If the word itself exceeds the limit and the line is empty,
        ///  word should be appended (receive its own line)</remarks>
        bool CanAppendToLine(string word, ICollection<string> wordsOnLine, int requiredLineLength);

        /// <summary>
        /// Calculates the necessary amount of spaces individually after each word to fill the required line size
        /// </summary>
        /// <param name="wordsToInclude">Words which need to be on the line</param>
        /// <param name="requiredLineLength"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        IEnumerable<int> InsertionSizes(ICollection<string> wordsToInclude, int requiredLineLength, Padding padding);

        /// <summary>
        /// Calculates the necessary amount of spaces individually after each word to fill the required line size
        /// </summary>
        /// <param name="wordsToIncludeLength"></param>
        /// <param name="wordsToIncludeCount"></param>
        /// <param name="requiredLineLength"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        IEnumerable<int> InsertionSizes(int wordsToIncludeLength, int wordsToIncludeCount, int requiredLineLength, Padding padding);

        /// <summary>
        /// Determine whether a character is appendable to any word
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CanAppendToWord(char value);
    }
}
