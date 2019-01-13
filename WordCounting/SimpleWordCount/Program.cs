using System;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;

namespace SimpleWordCount
{
    internal static class Program
    {
        private static readonly Regex PrintableSequenceMatcher = new Regex(@"\S+");
        //private static readonly Regex RealWordMatcher = new Regex(@"\w+");

        //private static int WordsOnLine(string line)
        //{
        //    return PrintableSequenceMatcher.Matches(line).Count;
        //}

        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Argument Error");
                return;
            }

            StreamReader input;
            string filePath = args[0];
            if (!TryLoadFile(filePath, out input))
            {
                Console.WriteLine("File Error");
                return;
            }

            int wordCount = new WordProcessor(input).CountWords();

            Console.WriteLine(wordCount); 
        }

        private static bool TryLoadFile(string filePath, out StreamReader input)
        {
            if (string.IsNullOrWhiteSpace(filePath) 
                || !File.Exists(filePath))
            {
                input = null;
                return false;
            }

            try
            {
                input = new FileInfo(filePath).OpenText();
                return true;
            }
            catch (Exception ex) when (
                ex is IOException
                || ex is SecurityException)
            {
                input = null;
                return false;
            }
        }
    }
}
