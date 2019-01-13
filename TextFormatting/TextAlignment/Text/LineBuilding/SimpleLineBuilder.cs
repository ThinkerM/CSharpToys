using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextAlignment.Contracts;
using TextAlignment.Extensions;

namespace TextAlignment.Text.LineBuilding
{
    /// <summary>
    /// Constructs lines with no special additional tweaks (left-based, simply glues all words and spaces)
    /// </summary>
    internal class SimpleLineBuilder : ILineBuilder
    {
        private const string InvalidWordsToSpacingRatioError 
            = "Invalid words and spacing amounts: amount of words has to be 1 larger than amount of spaces.";

        public string BuildLine(IEnumerable<string> words, IEnumerable<int> spacing)
        {
            if (!words.Any())
                return BuildEmptyLine();

            //avoid multiple enumeration
            List<string> wordsList = words.ToList(); 
            List<int> spacesList = spacing.ToList();

            wordsList.Validate(w => w.Count - 1 == spacesList.Count,
                new ArgumentException(InvalidWordsToSpacingRatioError 
                + $"{Environment.NewLine}Actual values - Words: {wordsList.Count}, Spaces: {spacesList.Count}."));

            return ConcatenateWords(wordsList, spacesList);
        }

        protected virtual string ConcatenateWords(IList<string> wordsList, List<int> spacesList)
        {
            var wordsWithSpaces = wordsList.Zip(spacesList, (word, spaces) => new {word, spaces});
            var line = new StringBuilder();

            foreach (var wordWithSpace in wordsWithSpaces)
            {
                line.Append(wordWithSpace.word);
                var spaces = new string(' ', wordWithSpace.spaces);
                line.Append(spaces);
            }

            //one word remaining (last word has no follow-up spaces, therefore not included in zip
            line.Append(wordsList.Last());
            return line.ToString();
        }

        public string BuildLine(IEnumerable<StringBuilder> words, IEnumerable<int> spacing)
            => BuildLine(words.Select(x => x.ToString()), spacing);

        public virtual string BuildEmptyLine() 
            => string.Empty;
    }
}
