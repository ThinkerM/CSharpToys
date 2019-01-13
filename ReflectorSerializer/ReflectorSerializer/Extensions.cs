using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectorSerializer
{
    internal static class Extensions
    {
        public static string ToXmlOpeningTag(this string value) => $"<{value}>";
        public static string ToXmlClosingTag(this string value) => $"</{value}>";

        public static void WriteLineOffset(this TextWriter writer, string value, int offsetCount, char offsetCharacter = ' ')
        {
            string offset = new string(offsetCharacter, offsetCount);
            writer.WriteLine(offset + value);
        }
    }
}
