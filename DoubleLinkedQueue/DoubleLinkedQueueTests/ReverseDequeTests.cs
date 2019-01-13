using System.Collections.Generic;
using System.Linq;
using DoubleLinkedQueue;
using NUnit.Framework;

namespace DoubleLinkedQueueTests
{
    [TestFixture]
    public class ReverseDequeTests
    {
        private static readonly char[] InitialValues = {'a', 'b', 'c', 'd'};
        private IDeque<char> original;
        private IDeque<char> reverse;

        [SetUp]
        public void InitializeDeques()
        {
            original = new Deque<char>(InitialValues);
            reverse = new ReverseDeque<char>(original);
        }

        [Test]
        public void PushLastTest()
        {
            reverse.PushLast('x');
            Assert.AreEqual('x', original.PeekFirst());

            original.PushLast('y');
            Assert.AreEqual('y', reverse.PeekFirst());
        }

        [Test]
        public void PushFirstTest()
        {
            reverse.PushFirst('w');
            Assert.AreEqual('w', original.PeekLast());

            original.PushFirst('z');
            Assert.AreEqual('z', reverse.PeekLast());
        }

        [Test]
        public void PopLastTest()
        {
            int reversePopped = reverse.PopLast();
            Assert.AreEqual(InitialValues[0], reversePopped);
            Assert.AreNotEqual(reversePopped, original[0]); //test that the value got removed from beginning of original

            int originalPopped = original.PopLast();
            Assert.AreNotEqual(originalPopped, reverse[0]);
        }

        [Test]
        public void PopFirstTest()
        {
            int reversePopped = reverse.PopFirst();
            Assert.AreEqual(InitialValues.Last(), reversePopped);
            Assert.AreNotEqual(reversePopped, original.Last()); //test that the value got removed from beginning of original

            int originalPopped = original.PopFirst();
            Assert.AreNotEqual(originalPopped, reverse.Last());
        }

        [Test]
        public void PeekLastTest()
        {
            Assert.AreEqual(InitialValues[0], reverse.PeekLast());
        }

        [Test]
        public void PeekFirstTest()
        {
            Assert.AreEqual(InitialValues.Last(), reverse.PeekFirst());
        }

        [Test]
        public void AddTest()
        {
            reverse.Add('.');
            var expectedReverseState = new List<char>(InitialValues.Reverse()) { '.' };
            CollectionAssert.AreEqual(expectedReverseState, reverse);

            var expectedOriginalState = new List<char> { '.' };
            expectedOriginalState.AddRange(InitialValues);
            CollectionAssert.AreEqual(expectedOriginalState, original);
        }

        [Test]
        public void ClearTest()
        {
            reverse.Clear();
            CollectionAssert.IsEmpty(reverse);
            CollectionAssert.IsEmpty(original);
        }

        [Test]
        public void ClearTest2()
        {
            original.Clear();
            CollectionAssert.IsEmpty(original);
            CollectionAssert.IsEmpty(reverse);
        }

        [Test]
        public void ContainsTest()
        {
            foreach (char value in InitialValues)
            {
                Assert.AreEqual(original.Contains(value), reverse.Contains(value));
            }
        }

        [Test]
        public void CopyToTest()
        {
            var originalCopy = new char[original.Count];
            var reverseCopy = new char[reverse.Count];
            original.CopyTo(originalCopy, 0);
            reverse.CopyTo(reverseCopy, 0);
            CollectionAssert.AreEqual(originalCopy.Reverse(), reverseCopy);
        }

        [Test]
        public void RemoveFromReverseTest()
        {
            Assume.That(reverse.Remove('a'));
            CollectionAssert.DoesNotContain(reverse, 'a');
            CollectionAssert.DoesNotContain(original, 'a');
        }

        [Test]
        public void RemoveFromOriginalTest()
        {
            Assume.That(original.Remove('c'));
            CollectionAssert.DoesNotContain(reverse, 'c');
        }

        [Test]
        public void IndexOfTest()
        {
            Assert.AreEqual(0, reverse.IndexOf('d'));
            Assert.AreEqual(1, reverse.IndexOf('c'));
            Assert.AreEqual(2, reverse.IndexOf('b'));
            Assert.AreEqual(3, reverse.IndexOf('a'));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void InsertTest(int index)
        {
            Assume.That(index.IsBetween(0, reverse.Count));
            var reverseReference = InitialValues.Reverse().ToList();
            reverse.Insert(index, ':');
            reverseReference.Insert(index, ':');
            CollectionAssert.AreEqual(reverseReference, reverse);
            reverseReference.Reverse();
            CollectionAssert.AreEqual(reverseReference, original);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RemoveAtTest(int index)
        {
            Assume.That(index.IsBetween(0, reverse.Count - 1));
            var reverseReference = InitialValues.Reverse().ToList();
            reverse.RemoveAt(index);
            reverseReference.RemoveAt(index);
            CollectionAssert.AreEqual(reverseReference, reverse);
            reverseReference.Reverse();
            CollectionAssert.AreEqual(reverseReference, original);
        }

        [Test(Description = "Check that reversing a reverse IDeque yields the original deque")]
        public void ReverseReverseTest() // :D :D
        {
            CollectionAssert.AreEqual(original, reverse.Reverse());
            CollectionAssert.AreEqual(original.Reverse(), reverse);
        }
    }
}