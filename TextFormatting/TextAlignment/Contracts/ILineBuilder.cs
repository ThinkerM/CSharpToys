using System.Collections.Generic;
using System.Text;

namespace TextAlignment.Contracts
{
    /// <summary>
    /// Provides functionality to construct single lines (i.e. strings of words terminated by a line terminator).
    /// </summary>
    public interface ILineBuilder
    {
        /// <summary>
        /// Construct a single line from a set of words, each followed by the given amount of spaces.
        /// <para>The amount of members of <see cref="spacing"/> has to be exactly one less than the amount of <see cref="words"/></para>
        /// </summary>
        /// <param name="words">Strings to be included in the result line</param>
        /// <param name="spacing">Information about the amount of spaces which should followe after every word</param>
        /// <returns></returns>
        string BuildLine(IEnumerable<string> words, IEnumerable<int> spacing);

        /// <summary>
        /// Construct a single line from a set of words, each followed by the given amount of spaces.
        /// <para>The amount of members of <see cref="spacing"/> has to be exactly one less than the amount of <see cref="words"/></para>
        /// </summary>
        /// <param name="words">Strings will be extracted from these to include in the result line</param>
        /// <param name="spacing">Information about the amount of spaces which should followe after every word</param>
        /// <returns></returns>
        string BuildLine(IEnumerable<StringBuilder> words, IEnumerable<int> spacing);

        string BuildEmptyLine();
    }
}
