using System.Collections.Generic;
using DoubleLinkedQueue;
using NUnit.Framework;

namespace DoubleLinkedQueueTests
{
    [TestFixture]
    public class RecodexImitationTests
    {
        private IDeque<int> deque;

        [SetUp]
        public void InitialiseDeque()
        {
            deque = new Deque<int>();
        }

        [Test]
        public void Foreach_Add_Test1()
        {
            foreach (int item in deque)
            {
                Assert.Fail("Deque is empty, no items should be returned in enumeration");
            }
            deque.Add(9);
            var list = new List<int>{ 9 };
            CollectionAssert.AreEqual(list, deque);
            foreach (int item in deque)
            {
                Assert.AreEqual(9, item); //only one item so far, any returned item should be the only added value
            }
            for (int i = 0; i < 150; i++)
            {
                deque.Add(i);
                list.Add(i);
            }
            CollectionAssert.AreEqual(list, deque);
        }

        [Test]
        public void Index_Count_Add_Test2()
        {
            for (int i = 0; i < 326; i++)
            {
                deque.Add(i);
                Assert.AreEqual(i + 1, deque.Count);
                Assert.AreEqual(i, deque[i]);
            }
            Assert.AreEqual(0, deque[0]);
            Assert.AreEqual(100, deque[100]);
            Assert.AreEqual(101, deque[101]);
            Assert.AreEqual(199, deque[199]);
        }

        [Test]
        public void Add_InsertFirst_Foreach_Count_Index_Test3()
        {
            var list = new List<int>();
            for (int i = 0; i < 232; i++)
            {
                deque.Add(i);
                list.Add(i);
                Assert.AreEqual(list[i], deque[i]);
                Assert.AreEqual(list.Count, deque.Count);
                CollectionAssert.AreEqual(list, deque); //acts as foreach on deque (enumerates it)

                deque.Insert(0, -i);
                list.Insert(0, -i);
                Assert.AreEqual(list[0], deque[0]);
                Assert.AreEqual(list.Count, deque.Count);
                CollectionAssert.AreEqual(list, deque);

                Assert.AreEqual(list[i / 2], deque[i / 2]);
            }
        }

        [Test]
        public void Remove_Add_InsertFirst_Foreach_Index_Count_Test4()
        {
            Assert.IsFalse(deque.Remove(0)); //no elements yet, can't remove
            Assert.AreEqual(0, deque.Count);
            deque.Insert(0, 0);
            Assert.AreEqual(0, deque[0]);
            Assert.AreEqual(1, deque.Count);
            foreach (int item in deque)
            {
                Assert.AreEqual(0, item); //only a single item so far
            }
            deque.Add(-1);
            CollectionAssert.AreEqual(new[]{ 0, -1 }, deque);
            Assert.IsTrue(deque.Remove(0));
            Assert.AreEqual(-1, deque[0]);
            Assert.IsTrue(deque.Remove(-1));
            Assert.AreEqual(0, deque.Count);
            deque.Insert(0, 1);
            deque.Insert(0, 0);
            deque.Add(2);
            CollectionAssert.AreEqual(new[]{ 0, 1, 2 }, deque);
        }

        [Test]
        public void IndexOf_Insert_Add_Test18()
        {
            Assert.AreEqual(-1, deque.IndexOf(0));
            deque.Insert(0, 0);
            Assert.AreEqual(0, deque.IndexOf(0));
            deque.Add(-1);
            CollectionAssert.AreEqual(new[] { 0, -1 }, deque);
            deque.Insert(2, 1);
            Assert.AreEqual(1, deque[2]);
            deque.Insert(2, 2);
            CollectionAssert.AreEqual(new[] { 0, -1 , 2, 1 }, deque);
        }
    }
}
