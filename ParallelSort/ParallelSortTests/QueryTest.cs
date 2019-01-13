using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using LibraryModel;
using MergeSortQuery;

namespace ParallelSortTests
{
    [TestFixture]
    public class QueryTest
    {
        private readonly Library testLibrary = new Library();
        private readonly List<FileInfo> createdFiles = new List<FileInfo>();
        private readonly FileInfo referenceFile = new FileInfo("testReference");

        [OneTimeSetUp]
        public void GenerateLibrary()
        {
            Console.WriteLine("<<Library setup>>");
            var rg = new RandomGenerator { RandomSeed = 42, BookCount = 2500000 / 2, ClientCount = 10000, LoanCount = 500000 * 5, MaxCopyCount = 10 };
            rg.FillLibrary(testLibrary);
            Console.WriteLine("<<Library setup finished>>");
        }

        [OneTimeSetUp]
        public void CreateReferencefile()
        {
            Console.WriteLine("<<Running reference query>>");
            QueryTester.RunQuery("reference parallel", ReferenceLibraryQueries.Queries.ParallelQuery, ReferenceLibraryQueries.Queries.PrintCopy, testLibrary, referenceFile.FullName);
            Console.WriteLine("<<Reference query completed>>");
        }

        [Test]
        public void Run0ThreadsTest()
        {
            Assert.Throws<InvalidOperationException>(() => new BookCopiesQuery {Library = testLibrary, ThreadCount = 0}
                                                         .ExecuteQuery());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void OutputDifferenceTest(int threads)
        {
            QueryTester.RunQuery(
                    $"MergeSort {threads:D2} threads",
                    lib => new BookCopiesQuery { Library = lib, ThreadCount = threads }.ExecuteQuery(),
                    ResultVisualizer.PrintCopy,
                    testLibrary,
                    $"{threads:D2} threads test"
                );
            var testOutput = new FileInfo($"{threads:D2} threads test");
            createdFiles.Add(testOutput);
            FileAssert.AreEqual(referenceFile, testOutput);
        }

        [TearDown]
        public void RemoveUsedFiles()
        {
            foreach (FileInfo file in createdFiles)
            {
                if (file.Exists) file.Delete();
            }
            createdFiles.Clear();
        }

        [OneTimeTearDown]
        public void RemoveReferenceFile()
        {
            if (referenceFile.Exists) referenceFile.Delete();
        }
    }
}
