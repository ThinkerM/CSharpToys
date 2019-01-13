using System;

namespace ExpressionEvaluationService.Parsing
{
    internal static class ParsingHelper
    {
        public static string[] GetTokens(string expressionString)
        {
            return expressionString.Split(
                ParsingConstants.ExpressionTokenSeparators,
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
