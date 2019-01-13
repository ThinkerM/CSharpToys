using NUnit.Framework;
using TableEditorCore;

namespace TableEditorCoreTests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        [TestCase(1, "A")]
        [TestCase(27, "AA")]
        [TestCase(703, "AAA")]
        [TestCase(702, "ZZ")]
        [TestCase(26, "Z")]
        public void ToExcelColumnTest(int number, string expectedColumn)
        {
            Assert.AreEqual(expectedColumn, number.ToExcelColumn());
        }

        [Test]
        [TestCase("A", 1)]
        [TestCase("a", 1)]
        [TestCase("ERROR", 2613824)]
        [TestCase("ZZ", 702)]
        public void AsOrdinalNumberTest(string column, int expectedNumber)
        {
            Assert.AreEqual(expectedNumber, column.AsOrdinalNumber());
        }

        [Test]
        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase("     ", true)]
        [TestCase("   ", true)]
        [TestCase("\n", true)]
        [TestCase("\t", true)]
        [TestCase("a", false)]
        [TestCase("  b  ", false)]
        public void IsNullOrWhitespaceTest(string value, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, value.IsNullOrWhitespace());
        }

        [Test]
        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase("     ", false)]
        [TestCase("   ", false)]
        [TestCase("\n\t", false)]
        [TestCase("\t", false)]
        [TestCase("a", false)]
        [TestCase("  b  ", false)]
        public void IsNullOrEmptyTest(string value, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, value.IsNullOrEmpty());

        }
    }
}