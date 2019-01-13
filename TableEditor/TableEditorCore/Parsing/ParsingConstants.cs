namespace TableEditorCore.Parsing
{
    internal static class ParsingConstants
    {
        /// <summary>
        /// Occurs when a formula has improper format - no operator present
        /// </summary>
        public const string MISSING_OPERATOR = @"#MISSOP";

        /// <summary>
        /// Occurs when a formula has improper format in any way other than missing operator (e.g. wrong operands coordinates format)
        /// </summary>
        public const string FORMULA_SYNTAX_ERROR = @"#FORMULA";

        /// <summary>
        /// Indicates that content of a cell wasn't a proper value (not a formula nor a number)
        /// </summary>
        public const string INVALID_VALUE = @"#INVVAL";

        /// <summary>
        /// Sequence indicating that a string is a formula
        /// </summary>
        public const string FORMULA_INITIAL_TOKEN = @"=";

        public const char SHEET_MARKER = '!';
    }
}