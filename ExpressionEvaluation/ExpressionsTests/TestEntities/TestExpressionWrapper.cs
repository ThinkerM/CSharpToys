using ExpressionsCore.Expressions;

namespace ExpressionsTests.TestEntities
{
    internal class TestExpressionWrapper
    {
        public TestExpressionWrapper(IExpression expression, string infixFullParenthesesFormat, string infixMinimalParenthesesFormat, int expectedIntegralResult)
        {
            Expression = expression;
            InfixFullParenthesesFormat = infixFullParenthesesFormat;
            InfixMinimalParenthesesFormat = infixMinimalParenthesesFormat;
            ExpectedIntegralResult = expectedIntegralResult;
        }

        public IExpression Expression { get; }

        public string InfixFullParenthesesFormat { get; }

        public string InfixMinimalParenthesesFormat { get; }

        public int ExpectedIntegralResult { get; }
    }
}
