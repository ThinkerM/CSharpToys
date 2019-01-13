using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TextAlignment.Contracts;
using TextAlignment.Enums;

namespace TextAlignment.Text.Tests
{
    [TestFixture]
    public class TextEvaluatorTests
    {
        private ITextEvaluator evaluator;

        [SetUp]
        public void SetUp()
        {
            evaluator = new TextEvaluator();
        }

        [Test]
        public void JustifiedInsertionSizesWithNumericalParametersTest()
        {
            int requiredLineLength = 9;
            int wordsLengthSum = 4;
            int wordsCount = 4;
            var expectedSpaces = new[] { 2, 2, 1 };
            CollectionAssert.AreEqual(expectedSpaces, evaluator.InsertionSizes(wordsLengthSum, wordsCount, requiredLineLength, Padding.Justified));

            requiredLineLength = 6;
            wordsLengthSum = 7;
            wordsCount = 2;
            expectedSpaces = new[] { 1 };
            CollectionAssert.AreEqual(expectedSpaces, evaluator.InsertionSizes(wordsLengthSum, wordsCount, requiredLineLength, Padding.Justified));

            requiredLineLength = 20;
            wordsLengthSum = 7;
            wordsCount = 1;
            Assert.IsFalse(evaluator.InsertionSizes(wordsLengthSum, wordsCount, requiredLineLength, Padding.Justified).Any());
        }

        [Test]
        public void JustifiedInsertionSizesWithWordCollectionParameterTest()
        {
            int requiredLineLength = 9;
            var words = new[] { "a", "b", "c", "d" };
            var expectedSpaces = new[] {2, 2, 1};
            CollectionAssert.AreEqual(expectedSpaces, evaluator.InsertionSizes(words, requiredLineLength, Padding.Justified));

            requiredLineLength = 5;
            words = new[] { "word1", "word2" };
            expectedSpaces = new[] {1};
            CollectionAssert.AreEqual(expectedSpaces, evaluator.InsertionSizes(words, requiredLineLength, Padding.Justified));

            requiredLineLength = 26;
            words = new[] { "word1", "word2", "word3", "word4" };
            expectedSpaces = new[] {2, 2, 2};
            CollectionAssert.AreEqual(expectedSpaces, evaluator.InsertionSizes(words, requiredLineLength, Padding.Justified));

            requiredLineLength = 30;
            words = new[] { "word1" };
            Assert.IsFalse(evaluator.InsertionSizes(words, requiredLineLength, Padding.Justified).Any());
        }

        [Test]
        [TestCase("word", new[] { "length7", "length7" }, 20, true)]
        [TestCase("a", new[] { "1", "2", "3" }, 5, false)]
        [TestCase("a", new[] { "1", "2", "3" }, 6, false)]
        [TestCase("a", new[] { "1", "2", "3" }, 7, true)]
        [TestCase("ab", new[] { "1", "2", "3" }, 7, false)]
        [TestCase("pretty-damn-longa$$-word", new string[0], 4, true)]
        [TestCase("word", new[] { "1" }, 5, false)]
        public void CanAppendToLineTest(string word, ICollection<string> wordsOnLine, int requiredLineLength, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, evaluator.CanAppendToLine(word, wordsOnLine, requiredLineLength));
        }

        [Test]
        [TestCase('a', true)]
        [TestCase('0', true)]
        [TestCase('=', true)]
        [TestCase('_', true)]
        [TestCase(' ', false)]
        [TestCase('\n', false)]
        [TestCase('\t', false)]
        public void CanAppendToWordTest(char value, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, evaluator.CanAppendToWord(value));
        }
    }
}