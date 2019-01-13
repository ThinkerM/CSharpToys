namespace TableEditorCore.Calculation
{
    internal static class CalculationConstants
    {
        /// <summary>
        /// Occurs when result cannot be determined due to one or more operands being impossible to calculate
        /// </summary>
        public const string CALCULATION_ERROR = @"#ERROR";

        /// <summary>
        /// Occurs when a division by 0 occurs within an expression
        /// </summary>
        public const string ZERO_DIVISION = @"#DIV0";

        /// <summary>
        /// Occurs when a set of operations cannot be computed due to being circularly dependent on one another
        /// </summary>
        public const string CYCLE = @"#CYCLE";
    }
}
