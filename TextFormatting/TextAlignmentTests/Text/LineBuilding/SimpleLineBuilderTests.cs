using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TextAlignment.Contracts;
using TextAlignment.Text.LineBuilding;

namespace TextAlignmentTests.Text.LineBuilding
{
    [TestFixture]
    public class LineBuilderTests
    {
        private ILineBuilder simpleBuilder;

        [SetUp]
        public void SetUp()
        {
            simpleBuilder = new SimpleLineBuilder();
        }

        [Test]
        public void SimpleBuildLineTest()
        {
            var spacing = new List<int> { 2, 2, 1, 1 };
            var words = new List<string> {"1", "2", "3", "4", "5"};
            string builtLine = simpleBuilder.BuildLine(words, spacing);
            string expectedLine = "1  2  3 4 5";
            Assert.AreEqual(expectedLine, builtLine);

            spacing = new List<int> { 2, 0, 1, 0 };
            words = new List<string> { "1", "2", "3", "4", "5" };
            builtLine = simpleBuilder.BuildLine(words, spacing);
            expectedLine = "1  23 45";
            Assert.AreEqual(expectedLine, builtLine);
        }

        [Test]
        public void SimpleBuildLineWithInvalidArgumentsTest()
        {
            var spacing = new List<int> { 2, 2, 1 };
            var words = new List<string> { "1", "2", "3", "4", "5" };
            Assert.Throws<ArgumentException>(() => simpleBuilder.BuildLine(words, spacing));

            spacing = new List<int> { 2, 1, -1, 0 };
            words = new List<string> { "1", "2", "3", "4", "5" };
            Assert.Throws<ArgumentOutOfRangeException>(() => simpleBuilder.BuildLine(words, spacing));
        }

        [Test]
        public void SimpleBuildEmptyLineTest()
        {
            var spacing = Enumerable.Empty<int>();
            var words = Enumerable.Empty<string>();

            string expectedResult = "";
            Assert.AreEqual(expectedResult, simpleBuilder.BuildEmptyLine());
            Assert.AreEqual(expectedResult, simpleBuilder.BuildLine(words, spacing));
        }
    }
}