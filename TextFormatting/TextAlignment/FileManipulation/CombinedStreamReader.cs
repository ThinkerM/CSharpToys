using System;
using System.Collections.Generic;
using System.IO;

namespace TextAlignment.FileManipulation
{
    /// <summary>
    /// "Concatenates" multiple files and imitates simplified behaviour of a single <see cref="StreamReader"/>
    /// </summary>
    internal class CombinedStreamReader : IDisposable
    {
        private readonly IList<string> filePaths;
        private StreamReader currentStream;
        private int currentFileIndex = -1;

        public CombinedStreamReader(IList<string> filePaths)
        {
            if (filePaths == null)
            {
                throw new ArgumentNullException(nameof(filePaths));
            }

            this.filePaths = filePaths;
            if (filePaths.Count > 0)
            {
                LoadNextFile();
            }
        }

        public bool EndOfCombinedStream { get; private set; } = false;

        public int Read()
        {
            if (currentStream == null)
            { return -1; }

            return InputValue(currentStream.Read);
        }

        public int Peek()
        {
            if (currentStream == null)
            { return -1; }

            return InputValue(currentStream.Peek);
        }

        private int InputValue(Func<int> inputProcessingMethod)
        {
            int processedStreamValue = inputProcessingMethod();
            if (processedStreamValue != -1)
            {
                return processedStreamValue;
            }

            LoadNextFile();
            return default(char); 
            //indicate file swap to prevent last and first words from neighbouring files being concatenated
        }

        private void LoadNextFile()
        {
            currentFileIndex++;
            if (currentFileIndex >= filePaths.Count)
            {
                EndOfCombinedStream = true;
                return;
            }

            currentStream?.Dispose();
            currentStream = LoadFileOnIndex(currentFileIndex);
            if (currentStream == null)
            {
                LoadNextFile();
            }
        }

        private StreamReader LoadFileOnIndex(int index)
        {
            if (index < 0 || index >= filePaths.Count)
                throw new ArgumentOutOfRangeException(nameof(index), index, $"Must be within range of {nameof(filePaths)}");

            try
            {
                return FileLoader.ForceOpenForReading(filePaths[index]);
            }
            catch (IOException)
            {
                return null;
            }
        }

        public void Dispose()
        {
            currentStream?.Dispose();
        }
    }
}