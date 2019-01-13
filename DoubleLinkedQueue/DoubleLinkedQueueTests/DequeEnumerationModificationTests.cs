using System;
using DoubleLinkedQueue;
using NUnit.Framework;

namespace DoubleLinkedQueueTests
{
    [TestFixture]
    public class DequeEnumerationModificationTests
    {
        private IDeque<int> deque;

        [SetUp]
        public void InitialiseDeque()
        {
            deque = new Deque<int>(0, 1, 2, 3, 4, 5);
        }

        [Test]
        public void PushLastDuringEnumerationTest()
        {
            AssertExceptionOnModification(() => deque.PushLast(5));
        }

        [Test]
        public void PushFirstDuringEnumerationTest()
        {
            AssertExceptionOnModification(() => deque.PushFirst(5));
        }

        [Test]
        public void PopLastDuringEnumerationTest()
        {
            AssertExceptionOnModification(() => deque.PopLast());
        }

        [Test]
        public void PopFirstDuringEnumerationTest()
        {
            AssertExceptionOnModification(() => deque.PopFirst());
        }

        [Test]
        public void AddDuringEnumerationTest()
        {
            AssertExceptionOnModification(() => deque.Add(5));
        }

        [Test]
        public void RemoveDuringEnumerationTest()
        {
            AssertExceptionOnModification(() => deque.Remove(1));
        }

        [Test]
        public void RemoveAtDuringEnumerationTest()
        {
            AssertExceptionOnModification(() => deque.RemoveAt(0));
        }

        [Test]
        public void ClearDuringEnumerationTest()
        {
            AssertExceptionOnModification(() => deque.Clear());
        }

        [Test]
        public void IndexerAccessDuringEnumerationTest()
        {
            AssertExceptionOnModification(() => deque[2] = 2);
        }

        [Test]
        public void InsertDuringModificationTest()
        {
            AssertExceptionOnModification(() => deque.Insert(1, 2));
        }

        private void AssertExceptionOnModification(Action modification)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                foreach (int value in deque)
                {
                    modification();
                }
            });
        }
    }
}
