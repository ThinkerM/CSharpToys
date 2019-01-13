using System;
using System.IO;
using System.Linq;
using System.Security;
using TextAlignment.Extensions;
using TextAlignment.FileManipulation;

namespace TextAlignment
{
    static partial class Program
    {
        private static bool ValidArguments(string[] args)
        {
            if (args.Any())
            {
                int requiredLineLength;
                int expectedLineLengthIndex = args.Length - 1; //last argument must indicate required line length

                if (args.MinimalAmount(args[0] == "--highlight-spaces" ? 4 : 3)
                    && args.ValidTypeOnIndex(typeof(int), expectedLineLengthIndex) 
                    && int.TryParse(args[expectedLineLengthIndex], out requiredLineLength)
                    && requiredLineLength > 0)
                {
                    return true;
                }
            }

            Console.WriteLine("Argument Error");
            return false;
        }

        private static bool CanLoadFiles(string inputFilePath, string outputFilePath)
        {
            try
            {
                FileLoader.OpenForReading(inputFilePath);
                FileLoader.OpenForWriting(outputFilePath);
            }
            catch (Exception e) when (
            e is IOException
            || e is SecurityException
            || e is UnauthorizedAccessException)
            {
                Console.WriteLine("File Error");
                return false;
            }
            return true;
        }
    }
}
