/**/
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TextAlignment.Contracts;

namespace TextAlignment.Text.LineBuilding.Tests
{
    [TestFixture]
    public class HighlightingLineBuilderTests
    {
        private ILineBuilder highlightingBuilder;

        [SetUp]
        public void SetUp()
        {
            highlightingBuilder = new HighlightingLineBuilder(SPACE, BOL, EOL);
        }

        /// <summary>
        /// Space highlighting symbol
        /// </summary>
        private const char SPACE = '.';

        /// <summary>
        /// End of line highligting sequence
        /// </summary>
        private const string EOL = "<-";

        /// <summary>
        /// Beginning of line highlighting sequence
        /// </summary>
        private const string BOL = "";

        [Test]
        public void BuildHighlightedLineTest()
        {
            var spacing = new List<int> { 2, 2, 1, 1 };
            var words = new List<string> { "1", "2", "3", "4", "5" };
            string builtLine = highlightingBuilder.BuildLine(words, spacing);
            string expectedLine = 
                $"{BOL}{words[0]}{SPACE}{SPACE}{words[1]}{SPACE}{SPACE}{words[2]}{SPACE}{words[3]}{SPACE}{words[4]}{EOL}";
            Assert.AreEqual(expectedLine, builtLine);

            spacing = new List<int> { 2, 0, 1, 0 };
            words = new List<string> { "1", "2", "3", "4", "5" };
            builtLine = highlightingBuilder.BuildLine(words, spacing);
            expectedLine = $"{BOL}{words[0]}{SPACE}{SPACE}{words[1]}{words[2]}{SPACE}{words[3]}{words[4]}{EOL}";
            Assert.AreEqual(expectedLine, builtLine);
        }

        [Test]
        public void BuildHighlightedLineWithInvalidArgumentsTest()
        {
            var spacing = new List<int> { 2, 2, 1 };
            var words = new List<string> { "1", "2", "3", "4", "5" };
            Assert.Throws<ArgumentException>(() => highlightingBuilder.BuildLine(words, spacing));

            spacing = new List<int> { 2, 1, -1, 0 };
            words = new List<string> { "1", "2", "3", "4", "5" };
            Assert.Throws<ArgumentOutOfRangeException>(() => highlightingBuilder.BuildLine(words, spacing));
        }

        [Test]
        public void BuildHighlightedEmptyLineTest()
        {
            var spacing = Enumerable.Empty<int>();
            var words = Enumerable.Empty<string>();

            string expectedResult = $"{BOL}{EOL}";
            Assert.AreEqual(expectedResult, highlightingBuilder.BuildEmptyLine());
            Assert.AreEqual(expectedResult, highlightingBuilder.BuildLine(words, spacing));
            //transitively, emptyLine1 and emptyLine2 will be equal too
        }
    }
}
/**/