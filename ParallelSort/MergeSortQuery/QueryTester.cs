using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LibraryModel;

namespace MergeSortQuery
{
    internal delegate IList<Copy> QueryDelegate(Library library);

    internal delegate void PrintCopyDelegate(StreamWriter writer, Copy copy);

    internal static class QueryTester
    {
        public static void RunQuery(string queryName, QueryDelegate query, PrintCopyDelegate printCopy, Library library, string fileName, bool printFiles = true)
        {
            Console.WriteLine("Executing {0} query ...", queryName);
            var swQuery = Stopwatch.StartNew();
            var result = query(library);
            swQuery.Stop();

            Stopwatch swPrint = null;
            if (printFiles)
            {
                Console.WriteLine("Printing query results ...");

                using (var writer = new System.IO.StreamWriter(fileName))
                {
                    swPrint = Stopwatch.StartNew();
                    foreach (var c in result)
                    {
                        printCopy(writer, c);
                    }
                    swPrint.Stop();
                }
            }

            Console.WriteLine($"Query time: {swQuery.Elapsed.TotalSeconds} s");
            Console.WriteLine($"Print time: {swPrint?.Elapsed.TotalSeconds} s");
            Console.WriteLine();
        }

        public static void RunQuery(string queryName, QueryDelegate query, PrintCopyDelegate printCopy, Library library, bool printFiles = true)
        {
            string fileName = CreateFileName(queryName);
            RunQuery(queryName, query, printCopy, library, fileName, printFiles);
        }

        private static string CreateFileName(string queryName)
        {
            var parts = queryName.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var fileName = new System.Text.StringBuilder("Result");
            Array.ForEach(parts, p =>
            {
                fileName.Append(char.ToUpperInvariant(p[0]));
                fileName.Append(p.Substring(1).ToLowerInvariant());
            });
            fileName.Append(".txt");
            return fileName.ToString();
        }
    }
}