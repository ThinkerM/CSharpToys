using System;
using System.ComponentModel;
using System.Linq;

namespace TextAlignment.Extensions
{
    public static class TextExtensions
    {
        public static bool CanConvertTo(this string value, Type type)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            return converter.IsValid(value);
        }

        public static bool IsNullOrWhitespace(this string value) 
            => string.IsNullOrWhiteSpace(value);

        private static readonly char[] NonPrintables = {' ', '\n', '\r', '\t', default(char)};

        public static bool IsPrintable(this char value)
            => !value.NotPrintable();

        public static bool NotPrintable(this char value)
            => NonPrintables.Contains(value);

        public static bool IsLineBreak(this char value) 
            => (value == '\n');
    }
}