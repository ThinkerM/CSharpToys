using System.IO;
using TextAlignment.Contracts;

namespace TextAlignment.FileManipulation
{
    /// <summary>
    /// Wraps instances of a single <see cref="StreamReader"/> and <see cref="StreamWriter"/> and exposes 
    /// a reduced subset of their functionality
    /// </summary>
    internal class StreamIoWrapper : IInputOutputWrapper
    {
        private readonly StreamReader input;
        private readonly StreamWriter output;

        public StreamIoWrapper(string inputPath, string outputPath)
        {
            input = FileLoader.OpenForReading(inputPath);
            output = FileLoader.OpenForWriting(outputPath);
            output.NewLine = "\n";
        }

        public StreamIoWrapper(StreamReader input, StreamWriter output)
        {
            this.input = input;
            this.output = output;
        }

        #region Wrapped Input
        public bool EndOfInput
            => input.EndOfStream;

        public bool HasInput
            => !input.EndOfStream;

        public char Read()
            => (char)input.Read();

        public char Peek()
            => (char)input.Peek();
        #endregion

        #region Wrapped Output
        public void WriteLine(string value = "")
        {
            output.WriteLine(value);
        }

        public void FlushOutput()
            => output.Flush();
        #endregion

        public void Dispose()
        {
            input?.Dispose();
            output?.Dispose();
        }
    }
}
