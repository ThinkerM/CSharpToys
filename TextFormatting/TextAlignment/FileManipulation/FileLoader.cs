using System;
using System.IO;
using System.Security;

namespace TextAlignment.FileManipulation
{
    internal static class FileLoader
    {
        /// <summary>
        /// Only opens the file if it exists and isn't under any security restrictions.
        /// If any of the conditions aren't met, throws an instance from the <see cref="IOException"/> hierarchy accordingly
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static StreamReader OpenForReading(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(nameof(filePath));
            return new StreamReader(filePath);
        }

        /// <summary>
        /// Only opens the file if it exists and isn't under any security restrictions.
        /// If any of the conditions aren't met, throws an instance from the <see cref="IOException"/> hierarchy accordingly
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static StreamWriter OpenForWriting(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(nameof(filePath));
            return new StreamWriter(filePath, false);
        }

        /// <summary>
        /// If the target file doesn't exist or is inacessible due to security or other reasons, returns null
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static StreamReader ForceOpenForReading(string filePath)
        {
            try
            {
                return new StreamReader(filePath);
            }
            catch (Exception ex) when (
                ex is IOException
                || ex is UnauthorizedAccessException)
            {
                return null;
            }
        }

        /// <summary>
        /// If target file doesn't exist, it will be created.
        /// If the target file isn't accessible due to security or other reasons, returns null
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static StreamWriter ForceOpenForWriting(string filePath)
        {
            return new StreamWriter(filePath, false);
        }
    }
}
