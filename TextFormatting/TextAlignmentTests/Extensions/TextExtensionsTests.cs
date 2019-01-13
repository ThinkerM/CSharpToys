using NUnit.Framework;
using System;

namespace TextAlignment.Extensions.Tests
{
    [TestFixture]
    public class TextExtensionsTests
    {
        [Test]
        [TestCase("word", typeof(string))]
        [TestCase("-10", typeof(int))]
        [TestCase("true", typeof(bool))]
        [TestCase("TRUE", typeof(bool))]
        public void CanConvertToWithValidTypesTest(string value, Type targeType)
        {
            Assert.IsTrue(value.CanConvertTo(targeType));
        }

        [Test]
        [TestCase("word", typeof(int))]
        [TestCase("-10014709192746923659649326494109", typeof(int))]
        [TestCase("ConsoleColor.BlackAndWhite", typeof(ConsoleColor))]
        [TestCase("1", typeof(bool))]
        public void CanConvertToWithInvalidTypesTest(string value, Type targeType)
        {
            Assert.IsFalse(value.CanConvertTo(targeType));
        }

        [Test]
        [TestCase("", true)]
        [TestCase("  ", true)]
        [TestCase("\n", true)]
        [TestCase("\n  \t", true)]
        [TestCase("\n  a \t", false)]
        [TestCase("   a", false)]
        [TestCase(null, true)]
        [TestCase("word", false)]
        public void IsNullOrWhitespaceTest(string value, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, value.IsNullOrWhitespace());
        }

        [Test]
        [TestCase('a', true)]
        [TestCase('\n', false)]
        [TestCase(' ', false)]
        [TestCase('\t', false)]
        [TestCase('1', true)]
        [TestCase('-', true)]
        [TestCase('_', true)]
        [TestCase('!', true)]
        [TestCase(default(char), false)]
        public void IsPrintableTest(char value, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, value.IsPrintable());
        }

        [Test]
        [TestCase('a', false)]
        [TestCase('\n', true)]
        [TestCase(' ', true)]
        [TestCase('\t', true)]
        [TestCase('1', false)]
        [TestCase('-', false)]
        [TestCase('_', false)]
        [TestCase('!', false)]
        [TestCase(default(char), true)]
        public void NotPrintableTest(char value, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, value.NotPrintable());
        }

        [Test]
        [TestCase('a', false)]
        [TestCase('\n', true)]
        [TestCase(' ', false)]
        [TestCase('\t', false)]
        [TestCase(default(char), false)]
        public void IsLineBreakTest(char value, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, value.IsLineBreak());
        }
    }
}