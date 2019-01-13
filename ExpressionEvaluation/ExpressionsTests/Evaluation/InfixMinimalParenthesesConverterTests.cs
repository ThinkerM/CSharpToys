using NUnit.Framework;
using ExpressionsTests.TestEntities;

namespace ExpressionsCore.Evaluation.Tests
{
    [TestFixture]
    public class InfixMinimalParenthesesConverterTests
    {
        private readonly InfixMinimalParenthesesConverter converter = new InfixMinimalParenthesesConverter();

        [Test]
        public void MinimalParenthesesConversionTest()
        {
            foreach (var expressionWrapper in TestExpressionsRepository.TestExpressions)
            {
                string convertedExpression = expressionWrapper.Expression.AcceptEvaluator(converter);
                Assert.AreEqual(expressionWrapper.InfixMinimalParenthesesFormat, convertedExpression);
            }
        }
    }
}