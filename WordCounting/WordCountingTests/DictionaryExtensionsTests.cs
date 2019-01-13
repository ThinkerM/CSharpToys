using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCountListing;

namespace WordCounting.Tests
{
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        private IDictionary<dynamic, int> dictionary;

        [SetUp]
        public void InitializeTest()
        {
            dictionary = new Dictionary<dynamic, int>();
        }

        [Test]
        [TestCase(1, 3)]
        [TestCase("key", -1)]
        [TestCase("key", 1)]
        [TestCase("key")]
        [TestCase(ConsoleColor.Blue, 10)]
        [TestCase("")]
        public void IncrementTest<T>(T key, int incrementValue = 1)
        {
            dictionary.Increment(key, incrementValue);
            Assert.AreEqual(incrementValue, dictionary[key]);
        }

        [Test]
        [TestCase("key")]
        [TestCase(2.255, 2)]
        [TestCase(ConsoleKey.E, -10)]
        [TestCase("key", 10000)]
        public void IncrementRepeatedlyTest<T>(T key, int incrementValue = 1)
        {
            dictionary.Increment(key, incrementValue);
            Assert.AreEqual(incrementValue, dictionary[key]);

            dictionary.Increment(key, incrementValue);
            dictionary.Increment(key, incrementValue);
            Assert.AreEqual(incrementValue * 3, dictionary[key]);
        }

        [Test]
        public void ParalellIncrementTest()
        {
            dictionary.Increment("key1", 10);
            dictionary.Increment("key2");
            Assert.AreEqual(10, dictionary["key1"]);
            Assert.AreEqual(1, dictionary["key2"]);

            dictionary.Increment("key1", -20);
            Assert.AreEqual(-10, dictionary["key1"]);
        }

        [Test]
        public void IncrementAndRemoveTest()
        {
            dictionary.Increment("key", 5);
            dictionary.Remove("key");
            dictionary.Increment("key", -10);
            Assert.AreEqual(-10, dictionary["key"]);
        }
    }
}