using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAlignment.Contracts;
using TextAlignment.Extensions;

namespace TextAlignment.Text.LineBuilding
{
    /// <summary>
    /// Marks all space characters, end and beginning of line with the configured values. 
    /// If spaces set to ' ' and others to empty strings, behaves the same as <see cref="SimpleLineBuilder"/>
    /// </summary>
    internal class HighlightingLineBuilder : SimpleLineBuilder
    {
        public HighlightingLineBuilder(char spaceHighlight = ' ', string beginningOfLineHighlight = "", string endOfLineHighlight = "")
        {
            SpaceHighlight = spaceHighlight;
            EndOfLineHighlight = endOfLineHighlight;
            BeginningOfLineHighlight = beginningOfLineHighlight;
        }

        public char SpaceHighlight { get; set; }
        public string EndOfLineHighlight { get; set; }
        public string BeginningOfLineHighlight { get; set; }

        protected override string ConcatenateWords(IList<string> wordsList, List<int> spacesList)
        {
            var wordsWithSpaces = wordsList.Zip(spacesList, (word, spaces) => new { word, spaces });
            var line = new StringBuilder();

            line.Append(BeginningOfLineHighlight);

            foreach (var wordWithSpace in wordsWithSpaces)
            {
                line.Append(wordWithSpace.word);
                var spaces = new string(SpaceHighlight, wordWithSpace.spaces);
                line.Append(spaces);
            }

            //one word remaining (last word has no follow-up spaces, therefore not included in zip
            line.Append(wordsList.Last());
            line.Append(EndOfLineHighlight);
            return line.ToString();
        }

        public override string BuildEmptyLine() 
            => BeginningOfLineHighlight + EndOfLineHighlight;
    }
}
