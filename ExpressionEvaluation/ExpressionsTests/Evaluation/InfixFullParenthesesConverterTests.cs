using NUnit.Framework;
using ExpressionsTests.TestEntities;

namespace ExpressionsCore.Evaluation.Tests
{
    [TestFixture]
    public class InfixFullParenthesesConverterTests
    {
        private readonly InfixFullParenthesesConverter converter = new InfixFullParenthesesConverter();

        [Test]
        public void FullParenthesesConversionTest()
        {
            foreach (var expressionWrapper in TestExpressionsRepository.TestExpressions)
            {
                string convertedExpression = expressionWrapper.Expression.AcceptEvaluator(converter);
                Assert.AreEqual(expressionWrapper.InfixFullParenthesesFormat, convertedExpression);
            }
        }
    }
}