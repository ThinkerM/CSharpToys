using System;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;

namespace WordCountListing
{
    internal static class Program
    {
        private const string ARGUMENT_ERROR = "Argument Error";
        private const string FILE_ERROR = "File Error";

        private static void Main(string[] args)
        {
            #region Input Check
            if (args.Length != 1)
            {
                Console.WriteLine(ARGUMENT_ERROR);
                return;
            }

            string filePath = args[0];
            StreamReader input;
            try
            {
                input = LoadFile(filePath).OpenText();
            }
            catch (Exception ex) when (
                 ex is FileNotFoundException
                 || ex is SecurityException
                 || ex is UnauthorizedAccessException
                 || ex is PathTooLongException)
            {
                Console.WriteLine(FILE_ERROR);
                return;
            }
            #endregion

            CountAllOccurences(input).Write(OutputMode.ByKey);
        }

        private static OutputableDictionary<string, int> CountAllOccurences(StreamReader input)
        {
            var output = new OutputableDictionary<string, int>();
            var reader = new WordReader(input);
            foreach (var word in reader.GetWords())
            {
                output.Increment(word);
            }
            return output;
        }

        private static FileInfo LoadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)
                || !File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            return new FileInfo(filePath);
        }
    }
}
