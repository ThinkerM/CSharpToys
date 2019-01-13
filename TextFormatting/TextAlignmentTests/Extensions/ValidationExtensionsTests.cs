using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TextAlignment.Extensions.Tests
{
    [TestFixture]
    public class ValidationExtensionsTests
    {
        [Test]
        [TestCase(new[] { "1" }, 1, true)]
        [TestCase(new[] { "0" }, 1, true)]
        [TestCase(new string[0], 6, false)]
        [TestCase(new string[0], 0, true)]
        [TestCase(new[] { "1", "2" }, 2, true)]
        public void ValidAmountTest(string[] arguments, int requiredAmount, bool shouldPass)
        {
            Assert.AreEqual(shouldPass, arguments.ValidAmount(requiredAmount));
        }

        [Test]
        [TestCase(new[] { "word", "10" }, typeof(string), 0, true)]
        [TestCase(new[] { "word", "10" }, typeof(int), 1, true)]
        [TestCase(new[] { "word", "-5" }, typeof(int), 1, true)]
        [TestCase(new[] { "word", "10" }, typeof(string), 1, true)]
        [TestCase(new[] { "word", "10" }, typeof(int), 0, false)]
        [TestCase(new string[0], typeof(string), 0, false)]
        [TestCase(new[] { "word", "10" }, typeof(int), 3, false)]
        [TestCase(new[] { "word", "10" }, typeof(long), 1, true)]
        public void ValidTypeOnIndexTest(string[] arguments, Type requiredType, int expectedIndex, bool shouldPass)
        {
            if (expectedIndex < 0 || expectedIndex >= arguments.Length)
            {
                Assert.ThrowsException<IndexOutOfRangeException>(() =>
                    arguments.ValidTypeOnIndex(requiredType, expectedIndex));
                return;
            }
            Assert.AreEqual(shouldPass, arguments.ValidTypeOnIndex(requiredType, expectedIndex));
        }

        [Test]
        public void ValidateExpectFailTest()
        {
            int intArgument = 0;
            var expectedException = new ArgumentOutOfRangeException();
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => intArgument.Validate(arg => arg > 0, expectedException));

            IEnumerable<string> arguments = Enumerable.Empty<string>();
            Assert.ThrowsException<ArgumentException>(
                () => arguments.Validate(args => args.Any(), new ArgumentException()));
        }

        [Test]
        public void ValidateExpectPassTest()
        {
            string stringArgument = "valid";
            stringArgument.Validate(arg => arg == "valid", new ArgumentException()); //shouldn't throw anything

            int[] intArguments = { 1,2,3,4,5 };
            intArguments.Validate(args => 
                (args.Contains(5) || args.Contains(-5))
                && (args.Length > 10 || args.Length < 6),
                new ArgumentException());
        }
    }
}