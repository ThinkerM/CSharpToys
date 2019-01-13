using System;
using System.IO;
using System.Text;
using ConsoleTester;
using NUnit.Framework;
using ReflectorSerializerTests.Properties;

namespace ReflectorSerializer.Tests
{
    [TestFixture]
    public class SerializerTests
    {
        private readonly Person testPerson = Person.TestInstance;
        private ISerializer testSerializer;

        [SetUp]
        public void InitializeSerializer() => testSerializer = new Serializer();

        [Test]
        public void SingleIntTest()
        {
            RunSerializationTest((s, output) => s.Serialize(output, 42), Resources.SingleInt);
        }

        [Test]
        public void SingleStringTest()
        {
            RunSerializationTest((s, output) => s.Serialize(output, "Hello"), Resources.SingleString);
        }

        [Test]
        public void DefaultIndentTest()
        {
            RunSerializationTest((s, output) => s.Serialize(output, testPerson), Resources.DefaultIndent);
        }

        [Test]
        public void IndentBy4Test()
        {
            RunSerializationTest((s, output) =>
            {
                s.IndentSpaceCount = 4;
                s.Serialize(output, testPerson);
            },
            Resources.IndentBy4);
        }

        [Test]
        public void IndentBy2EnumerableStringTest()
        {
            RunSerializationTest((s, output) =>
            {
                s.IndentSpaceCount = 2;
                s.TreatStringAsEnumerable = true;
                s.Serialize(output, testPerson);
            },
            Resources.Indent2EnumerableString);
        }

        private void RunSerializationTest(Action<ISerializer, TextWriter> serialization, string expectedOutput)
        {
            var result = new StringBuilder();
            using (var output = new StringWriter(result))
            {
                serialization(testSerializer, output);
            }
            Assert.AreEqual(expectedOutput, result.ToString());
        }
    }
}