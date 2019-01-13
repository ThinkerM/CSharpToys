using System;
using System.Collections.Generic;
using System.Linq;
using DoubleLinkedQueue;
using NUnit.Framework;

namespace DoubleLinkedQueueTests
{
    [TestFixture]
    public class DequeTests
    {
        private const int DATA_SET_SIZE = 113;
        private static readonly int[] InitialValues = Enumerable.Range(0, DATA_SET_SIZE).ToArray();
        private IDeque<int> testDeque;
        private IDeque<int> TestDeque => testDeque ?? (testDeque = new Deque<int>(InitialValues));

        [TearDown]
        public void RemoveDeque()
        {
            testDeque = null;
        }

        [Test]
        public void PushLastTest()
        {
            TestDeque.PushLast(100);
            Assert.AreEqual(InitialValues.Length + 1, TestDeque.Count);
            Assert.AreEqual(100, TestDeque[TestDeque.Count - 1]);
        }

        [Test]
        public void PushLastMultipleTest()
        {
            var deque = new Deque<int>();
            for (int i = 0; i < 100; i++)
            {
                deque.PushLast(i);
                Assert.AreEqual(i + 1, deque.Count);
                Assert.AreEqual(i, deque[i]);
            }
        }

        [Test]
        public void PushLastMultipleAndEnumerateTest()
        {
            var deque = new Deque<int>();
            var values = Enumerable.Range(0, 1313).ToArray();
            foreach (int value in values)
            {
                deque.PushLast(value);
            }
            CollectionAssert.AreEqual(values, deque);
        }

        [Test]
        public void PushLastMultipleAndEnumerateThroughoutTest()
        {
            var deque = new Deque<int>();
            var reference = new List<int>();
            var values = Enumerable.Range(0, 1313).ToArray();
            foreach (int value in values)
            {
                deque.PushLast(value);
                reference.Add(value);
                CollectionAssert.AreEqual(reference, deque);
            }
            CollectionAssert.AreEqual(values, deque);
        }

        [Test]
        public void PushFirstTest()
        {
            TestDeque.PushFirst(100);
            Assert.AreEqual(InitialValues.Length + 1, TestDeque.Count);
            Assert.AreEqual(100, TestDeque[0]);
        }

        [Test]
        public void PushFirstMultipleTest()
        {
            var deque = new Deque<int>();
            for (int i = 0; i < 100; i++)
            {
                deque.PushFirst(i);
                Assert.AreEqual(i + 1, deque.Count);
                Assert.AreEqual(i, deque[0]);
            }
        }

        [Test]
        public void PushFirstMultipleAndEnumerateTest()
        {
            var deque = new Deque<int>();
            var values = Enumerable.Range(0, 323).ToArray();
            foreach (int value in values)
            {
                deque.PushFirst(value);
            }
            CollectionAssert.AreEqual(values.Reverse(), deque);
        }

        [Test]
        public void PushFirstMultipleAndEnumerateThroughoutTest()
        {
            var deque = new Deque<int>();
            var reference = new List<int>();
            var values = Enumerable.Range(0, 323).ToArray();
            foreach (int value in values)
            {
                deque.PushFirst(value);
                reference.Insert(0, value);
                CollectionAssert.AreEqual(reference, deque);
            }
            CollectionAssert.AreEqual(values.Reverse(), deque);
        }

        [Test]
        public void PushLastThenFirstTest()
        {
            var deque = new Deque<string>();
            var reference = new List<string>();

            deque.PushLast("last");
            reference.Add("last");
            Assert.AreEqual(1, deque.Count);
            Assert.AreEqual("last", deque[0]);
            CollectionAssert.AreEqual(reference, deque);

            deque.PushFirst("first");
            reference.Insert(0, "first");
            Assert.AreEqual(2, deque.Count);
            Assert.AreEqual("first", deque[0]);
            CollectionAssert.AreEqual(reference, deque);
        }

        [Test]
        public void PushFirstThenLastTest()
        {
            var deque = new Deque<string>();
            var reference = new List<string>();

            deque.PushFirst("first");
            reference.Insert(0, "first");
            Assert.AreEqual(1, deque.Count);
            Assert.AreEqual("first", deque[0]);
            CollectionAssert.AreEqual(reference, deque);

            deque.PushLast("last");
            reference.Add("last");
            Assert.AreEqual(2, deque.Count);
            Assert.AreEqual("last", deque[1]);
            CollectionAssert.AreEqual(reference, deque);
        }

        [Test]
        public void PushMultiple_BothEnds()
        {
            var deque = new Deque<int>();
            var reference = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                deque.PushFirst(i);
                deque.PushLast(-i);
                Assert.AreEqual(-i, deque[deque.Count - 1]);
                Assert.AreEqual(i, deque[0]);

                reference.Insert(0, i);
                reference.Add(-i);
                CollectionAssert.AreEqual(reference, deque);
            }
        }

        [Test]
        public void PushMultiple_BothEnds2()
        {
            var deque = new Deque<int>();
            var reference = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                deque.PushLast(i);
                deque.PushFirst(-i);
                Assert.AreEqual(i, deque[deque.Count - 1]);
                Assert.AreEqual(-i, deque[0]);

                reference.Insert(0, -i);
                reference.Add(i);
                CollectionAssert.AreEqual(reference, deque);
            }
        }

        [Test]
        public void PopLastTest()
        {
            Assert.AreEqual(InitialValues[InitialValues.Length - 1],
                            TestDeque.PopLast());
        }

        [Test]
        public void PopFirstTest()
        {
            Assert.AreEqual(InitialValues[0],
                            TestDeque.PopFirst());
        }

        [Test]
        public void PopMultipleLastTest()
        {
            var reference = new List<int>(InitialValues);
            for (int i = InitialValues.Length - 1; i >= 0; i--)
            {
                int popped = TestDeque.PopLast();
                Assert.AreEqual(InitialValues[i], popped);

                reference.RemoveAt(reference.Count - 1);
                CollectionAssert.AreEqual(reference, TestDeque);
            }
        }

        [Test]
        public void PopMultipleFirstTest()
        {
            var reference = new List<int>(InitialValues);
            foreach (int value in InitialValues)
            {
                int popped = TestDeque.PopFirst();
                Assert.AreEqual(value, popped);

                reference.RemoveAt(0);
                CollectionAssert.AreEqual(reference, TestDeque);
            }
        }

        [Test]
        public void PopMultiple_BothEnds()
        {
            var deque = new Deque<char>('a', 'b', 'c', 'd', 'e', 'f');
            Assert.AreEqual('a', deque.PopFirst());
            Assert.AreEqual('f', deque.PopLast());
            Assert.AreEqual('b', deque.PopFirst());
            Assert.AreEqual('e', deque.PopLast());
            Assert.AreEqual('c', deque.PopFirst());
            Assert.AreEqual('d', deque.PopLast());
            Assert.AreEqual(0, deque.Count);
        }

        [Test]
        public void PopEmptyTest()
        {
            var emptyDeque = new Deque<bool>();
            Assert.Throws<InvalidOperationException>(() => emptyDeque.PopFirst());
            Assert.Throws<InvalidOperationException>(() => emptyDeque.PopLast());

        }

        [Test]
        public void PeekLastTest()
        {
            Assert.AreEqual(InitialValues[InitialValues.Length - 1], 
                            TestDeque.PeekLast());
        }

        [Test]
        public void PeekFirstTest()
        {
            Assert.AreEqual(InitialValues[0], 
                            TestDeque.PeekFirst());
        }

        [Test]
        public void PeekEmptyTest()
        {
            var emptyDeque = new Deque<bool>();
            Assert.Throws<InvalidOperationException>(() => emptyDeque.PeekFirst());
            Assert.Throws<InvalidOperationException>(() => emptyDeque.PeekLast());
        }

        [Test(Description = 
            "Ensure that all elements can be properly peeked at by popping through the whole structure and peeking each element")]
        public void PeekAndPopFirstMultipleTest()
        {
            for (int i = 0; i < TestDeque.Count; i++)
            {
                TestDeque.PeekFirst();
                TestDeque.PopFirst();
            }
        }

        [Test(Description =
            "Ensure that all elements can be properly peeked at by popping through the whole structure and peeking each element")]
        public void PeekAndPopLastMultipleTest()
        {
            for (int i = 0; i < TestDeque.Count; i++)
            {
                TestDeque.PeekLast();
                TestDeque.PopLast();
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(6)]
        [TestCase(98)]
        [TestCase(DATA_SET_SIZE - 1)]
        public void AccessIndexTest(int index)
        {
            Assume.That(index.IsBetween(0, InitialValues.Length - 1), $"Value of {nameof(index)} must be within bounds of {nameof(InitialValues)}.");
            int retrievedValue = TestDeque[index];
            Assert.AreEqual(InitialValues[index], retrievedValue);
        }

        [Test]
        public void AccessAllIndicesTest()
        {
            for (int i = 0; i < InitialValues.Length; i++)
            {
                Assert.AreEqual(InitialValues[i], TestDeque[i]);
            }
        }

        [Test]
        [TestCase(DATA_SET_SIZE)]
        [TestCase(DATA_SET_SIZE + 1)]
        [TestCase(-1)]
        public void AccessIndexOutOfRangeTest(int index)
        {
            Assume.That(index >= InitialValues.Length || index < 0, $"Testcase value not out of bounds of {nameof(InitialValues)}, cannot expect an exception to be thrown.");
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                int outOfRangeValue = TestDeque[index];
            });
        }

        [Test]
        public void ReverseEnumerationTest()
        {
            using (var reverseEnumerator = TestDeque.GetReverseEnumerator())
            {
                for (int i = InitialValues.Length - 1; i >= 0; i--)
                {
                    reverseEnumerator.MoveNext();
                    Assert.AreEqual(InitialValues[i], reverseEnumerator.Current);
                }
            }
        }

        [Test]
        public void RemoveAtTest()
        {
            var deque = new Deque<int>(0, 1, 2, 3, 4);
            deque.RemoveAt(2);
            CollectionAssert.AreEqual(new[]{0, 1, 3, 4}, deque);
        }

        [Test]
        public void RemoveAtMultipleTest()
        {
            var reference = InitialValues.ToList();
            while (TestDeque.Count > 0)
            {
                int removalIndex = TestDeque.Count / 2; //take approximately the middle
                TestDeque.RemoveAt(removalIndex);
                reference.RemoveAt(removalIndex);
                CollectionAssert.AreEqual(reference, TestDeque);
            }
        }

        [Test]
        public void RemoveTest()
        {
            var reference = new List<string>{ "John", "Ann", "Mary", "Edward" };
            var deque = new Deque<string>(reference.ToArray());
            reference.Remove("Mary");
            deque.Remove("Mary");
            CollectionAssert.AreEqual(reference, deque);
        }

        [Test]
        public void RemoveFromEmptyDequeTest()
        {
            var emptyDeque = new Deque<int>();
            Assert.IsFalse(emptyDeque.Remove(0));
        }

        [Test]
        public void IndexOfTest()
        {
            var deque = new Deque<int>(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);
            for (int i = 0; i < 14; i++)
            {
                Assert.AreEqual(i, deque.IndexOf(i));
            }
        }

        [Test]
        public void IndexOfEmptyDequeTest()
        {
            var emptyDeque = new Deque<string>();
            Assert.AreEqual(-1, emptyDeque.IndexOf("s"));
        }

        [Test]
        public void IndexOfMultiple_PushLast()
        {
            var deque = new Deque<int>();
            for (int i = 0; i < 245; i++)
            {
                deque.PushLast(i);
                Assert.AreEqual(i, deque.IndexOf(i));
            }
        }

        [Test]
        public void IndexOfMultiple_PushFirst()
        {
            var deque = new Deque<int>();
            for (int i = 0; i < 312; i++)
            {
                deque.PushFirst(i);
                Assert.AreEqual(i, deque.IndexOf(0)); //0 should be staying in last position, which should have index of current 'i'
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void InsertTest(int index)
        {
            var reference = new List<char> { 'a', 'b', 'c', 'w', 'x' };
            var deque = new Deque<char>(reference.ToArray());
            Assume.That(index.IsBetween(0, deque.Count), "This test expects 'nice' values - all indices should be within range of the deque.");
            reference.Insert(index, ':');
            deque.Insert(index, ':');
            CollectionAssert.AreEqual(reference, deque);
        }

        [Test]
        public void AddTest()
        {
            TestDeque.Add(-100);
            Assert.AreEqual(-100, TestDeque[TestDeque.Count - 1]);
        }

        [Test]
        public void ClearTest()
        {
            TestDeque.Clear();
            Assert.IsEmpty(TestDeque);
            Assert.AreEqual(0, TestDeque.Count);
        }

        [Test]
        public void AddThenPopTillEmptyThenAddTest()
        {
            var deque = new Deque<int>();
            for (int i = 0; i < 100; i++)
            {
                deque.Add(i);
            }
            CollectionAssert.AreEqual(Enumerable.Range(0, 100), deque);
            for (int i = 0; i < 100; i++)
            {
                deque.PopLast();
            }
            CollectionAssert.IsEmpty(deque);
            for (int i = 0; i < 10; i++)
            {
                deque.Add(i);
            }
            CollectionAssert.AreEqual(Enumerable.Range(0, 10), deque);
        }

        [Test]
        public void AddThenClearThenAddTest()
        {
            var deque = new Deque<int>();
            for (int i = 0; i < 100; i++)
            {
                deque.Add(i);
            }
            CollectionAssert.AreEqual(Enumerable.Range(0, 100), deque);
            deque.Clear();
            CollectionAssert.IsEmpty(deque);
            for (int i = 0; i < 100; i++)
            {
                deque.Add(i);
                Assert.AreEqual(i, deque[i]);
            }
            CollectionAssert.AreEqual(Enumerable.Range(0, 100), deque);
        }

        [Test]
        public void IndexOfNullValues()
        {
            var deque = new Deque<string>();
            for (int i = 0; i < 534; i++)
            {
                deque.Add(null);
                Assert.AreEqual(0, deque.IndexOf(null));
            }
        }
    }
}