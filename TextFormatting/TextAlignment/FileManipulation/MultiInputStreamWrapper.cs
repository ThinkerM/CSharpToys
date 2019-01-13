using System.Collections.Generic;
using System.IO;
using TextAlignment.Contracts;

namespace TextAlignment.FileManipulation
{
    /// <summary>
    /// Allows for multiple input files to be loaded in a series while looking as a single stream
    /// </summary>
    internal class MultiInputStreamWrapper : IInputOutputWrapper
    {
        private readonly CombinedStreamReader input;
        private readonly StreamWriter output;

        public MultiInputStreamWrapper(IList<string> inputFilePaths, StreamWriter outputFile)
        {
            input = new CombinedStreamReader(inputFilePaths);
            output = outputFile;
            output.NewLine = "\n";
        }

        public bool EndOfInput
            => input.EndOfCombinedStream;

        public bool HasInput
            => !EndOfInput;

        public char Read()
            => (char) input.Read();

        public char Peek()
            => (char) input.Peek();

        public void WriteLine(string value = "")
            => output.WriteLine(value);

        public void FlushOutput()
            => output.Flush();

        public void Dispose()
        {
            input?.Dispose();
            output?.Dispose();
        }
    }
}
