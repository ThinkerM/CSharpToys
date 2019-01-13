namespace CommonLibrary
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
            => string.IsNullOrEmpty(value);

        public static bool IsNullOrWhitespace(this string value)
            => string.IsNullOrWhiteSpace(value);

        public static string TrimFirst(this string value, int count = 1)
            => value.Substring(count);

        public static string SurroundWith(this string value, string surroundLeft, string surroundRight)
            => surroundLeft + value + surroundRight;

        public static string SurroundWith(this string value, string symmetricSurround)
            => value.SurroundWith(symmetricSurround, symmetricSurround);

        public static string SurroundWithParentheses(this string value)
            => value.SurroundWith("(", ")");
    }
}
