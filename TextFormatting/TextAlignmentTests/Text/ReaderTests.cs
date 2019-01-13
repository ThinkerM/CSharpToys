using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextAlignment.Contracts;

namespace TextAlignment.Text.Tests
{
    [TestFixture]
    public class ReaderTests
    {
        private IInputOutputWrapper io;
        private IReader reader;
        private IList<string> wordsInInput;
        private int paragraphSeparationsInInput;

        private static readonly char[] SeparatorCharacters = { ' ', '\t', '\n' };

        [SetUp]
        public void InitializeReading()
        {
            const string INPUT = " \t Stew and rum is omni-present, much like candy. A token of gratitude slips on a banana peel. "
                                 + "A stumbling first step will take you to places you never expected not to visit. Trickery approaches at high velocity!"
                                 + "A small mercy is still not very coherent. Whiskey on the table comes asking for bread. "
                                 + "Nothing of importance slips on a banana peel. A passionate evening has its world rocked by trees (or rocks). "
                                 + "Nihilism sees the sun. Passion or serendipity is always a pleasure.\n\n "
                                 + "That way could please even the most demanding follower of Freud. A passionate evening is not yet ready to die. "
                                 + "Stew and rum would kindly inquire something about you. Trickery wants to go to hell. A cranky old lady wants to go to hell. "
                                 + "That stolen figurine tenderly sees to her child.\n"
                                 + "Too long a stick asked you a question? A shooting star wants to set things right. Nothingness is ever present. "
                                 + "A glittering gem is still not very coherent.";

            wordsInInput = INPUT.Split(SeparatorCharacters, StringSplitOptions.RemoveEmptyEntries);
            paragraphSeparationsInInput = 1;

            io = new TestIoWrapper(INPUT);
            reader = new Reader(io);
        }

        [TearDown]
        public void ReleaseResources()
        {
            io.Dispose();
        }

        [Test]
        public void NextPrintableTest()
        {
            var inputPrintableCharacters = wordsInInput.SelectMany(word => word.ToCharArray());
            foreach (var printableCharacter in inputPrintableCharacters)
            {
                Assert.AreEqual(printableCharacter, (char)reader.NextPrintable());
            }
            Assert.IsTrue(reader.NextPrintable().EndOfFile);
        }

        [Test]
        public void NextWordTest()
        {
            foreach (var word in wordsInInput)
            {
                Assert.AreEqual(word, (string)reader.NextWord());
            }
            Assert.IsTrue(reader.NextWord().EndOfFile);
        }

        [Test]
        public void ReadWordsTest()
        {
            CollectionAssert.AreEqual(wordsInInput, reader.ReadWords());
        }

        [Test]
        public void SkipWhitespacesTest()
        {
            reader.SkipWhitespaces();
            char firstCharInFirstWord = wordsInInput.First().First();
            Assert.IsTrue(io.Peek() == firstCharInFirstWord || io.EndOfInput);
        }

        [Test]
        public void NewParagraphEventRaisedTest()
        {
            int paragraphSeparationsEncountered = 0;
            reader.NewParagraphEncountered += (sender, args) 
                => paragraphSeparationsEncountered++;

            reader.ReadWords().ToList(); //force evaluation
            Assert.AreEqual(paragraphSeparationsInInput, paragraphSeparationsEncountered);
        }

        private class TestIoWrapper : IInputOutputWrapper
        {
            private readonly TextReader reader;
            private readonly TextWriter writer = new StringWriter();

            public TestIoWrapper(string input)
            {
                reader = new StringReader(input);
            }

            public bool EndOfInput
                => reader.Peek() == -1;

            public bool HasInput
                => !EndOfInput;

            public char Read()
                => (char)reader.Read();

            public char Peek()
                => (char)reader.Peek();

            public void WriteLine(string value = "") 
                => writer.Write($"{value}\n");

            public void FlushOutput() 
                => writer.Flush();

            public void Dispose()
            {
                reader?.Dispose();
                writer?.Dispose();
            }
        }
    }
}