using System;
using System.IO;

namespace TextAlignment.Contracts
{
    /// <summary>
    /// Encapsulates any implementation of <see cref="TextReader"/> and <see cref="TextWriter"/>
    /// to provide a common functional facade.
    /// </summary>
    internal interface IInputOutputWrapper : IDisposable
    {
        bool EndOfInput { get; }
        bool HasInput { get; }
        char Read();
        char Peek();
        void WriteLine(string value = "");
        void FlushOutput();
    }
}
