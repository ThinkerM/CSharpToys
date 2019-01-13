using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using LibraryModel;

namespace MergeSortQuery
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            bool printFiles = !args.Contains("-n") && (args.Contains("-p") || PrintFiles());

            Debug.Listeners.Add(new ConsoleTraceListener());
            var library = new Library();

            // Requires ~ 800 MB of RAM
            //RandomGenerator rg = new RandomGenerator { RandomSeed = 42, BookCount = 2500000 / 4, ClientCount = 10000, LoanCount = 500000 * 5, MaxCopyCount = 10 };

            // Requires ~ 1500 MB of RAM
            RandomGenerator rg = new RandomGenerator { RandomSeed = 42, BookCount = 2500000 / 2, ClientCount = 10000, LoanCount = 500000 * 5, MaxCopyCount = 10 };

            // Requires ~ 5000 MB of RAM	
            //RandomGenerator rg = new RandomGenerator { RandomSeed = 42, BookCount = 2500000 * 2, ClientCount = 10000, LoanCount = 500000 * 10, MaxCopyCount = 10 };

            Console.WriteLine("Generating library content ...");
			rg.FillLibrary(library);

			QueryTester.RunQuery("reference single threaded", ReferenceLibraryQueries.Queries.SingleThreadedQuery, ReferenceLibraryQueries.Queries.PrintCopy, library, printFiles);

			QueryTester.RunQuery("reference parallel", ReferenceLibraryQueries.Queries.ParallelQuery, ReferenceLibraryQueries.Queries.PrintCopy, library, printFiles);

			for (int threads = 1; threads <= 8; threads++)
            {
				QueryTester.RunQuery(
				    $"MergeSort {threads:D2} threads",
					lib => new BookCopiesQuery { Library = lib, ThreadCount = threads }.ExecuteQuery(),
					ResultVisualizer.PrintCopy,
					library,
                    printFiles
				);
			}

            #if DEBUG
            Console.ReadKey(true);
            #endif
        }

        private static bool PrintFiles()
        {
            Console.WriteLine("Print results to files? [Y/N]");
            return Console.ReadKey(true).Key == ConsoleKey.Y;
        }
	}

    internal static class ResultVisualizer
    {
        public static void PrintCopy(StreamWriter writer, Copy c)
        {
            writer.WriteLine("{0} {1}: {2} loaned to {3}, {4}.", c.OnLoan.DueDate.ToShortDateString(), c.Book.Shelf, c.Id, c.OnLoan.Client.LastName, System.Globalization.StringInfo.GetNextTextElement(c.OnLoan.Client.FirstName));
        }
    }
}
