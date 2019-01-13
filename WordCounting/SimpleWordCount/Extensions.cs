namespace SimpleWordCount
{
    internal static class Extensions
    {
        public static bool IsPrintable(this char value) 
            => (value != ' ' && value != '\t' && value != '\n' && value != '\r');

        public static bool NotPrintable(this char value)
            => !value.IsPrintable();
    }
}