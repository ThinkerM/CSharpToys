using System.Linq;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace WordCounting.Tests
{
    [TestFixture]
    public class RegexTests
    {
        private static readonly Regex Matcher = new Regex(@"\S+");

        [Test]
        [TestCase("", 0)]
        [TestCase("Word", 1)]
        [TestCase("wordWith100digits", 1)]
        [TestCase("Under_Scored_Word", 1)]
        [TestCase("Hyphenated-word", 1)]
        [TestCase("Slash/Separated Word", 2)]
        [TestCase("a a", 2)]
        [TestCase("a, ab.", 2)]
        [TestCase("This is a proper sentence.", 5)]
        [TestCase("Multiple, words, separated, by ! delimiters", 6)]
        [TestCase("numbers are words too: 5.", 5)]
        public void MatchCountTest(string input, int expectedCount)
        {
            Assert.AreEqual(expectedCount, Matcher.Matches(input).Count);
        }

        [Test]
        [TestCase("This is a proper sentence.", new []{"This", "is", "a", "proper", "sentence."})]
        [TestCase("Multiple, words, separated, by ! delimiters", new[] { "Multiple,", "words,", "separated,", "by", "delimiters", "!" })]
        [TestCase("Word word.", new[] { "Word", "word." })]
        public void ExactMatchesTest(string input, string[] expectedMatches)
        {
            var matches = Matcher.Matches(input)
                .OfType<Match>()
                .Select(m => m.Value)
                .ToArray();
            CollectionAssert.AreEquivalent(expectedMatches, matches);
        }
    }
}