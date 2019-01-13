using System;
using System.IO;

namespace TableEditorCore.Services
{
    internal static class FileLoader
    {
        public static FileInfo LoadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)
                || !File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            return new FileInfo(filePath);
        }

        public static bool TryLoadFile(string filePath, out FileInfo loadedFile)
        {
            if (!File.Exists(filePath))
            {
                loadedFile = null;
                return false;
            }

            loadedFile = new FileInfo(filePath);
            return true;
        }

        public static bool TryOpenFileText(string filePath, out TextReader fileText)
        {
            try
            {
                fileText = File.OpenText(filePath);
                return true;
            }
            catch (IOException)
            {
                fileText = null;
                return false;
            }
        }

        public static FileInfo GetOrCreateFile(string filePath)
        {
            FileInfo resultFile = new FileInfo(filePath);
            if (!File.Exists(filePath))
            {
                resultFile.Create().Dispose();
            }

            return resultFile;
        }
    }
}
