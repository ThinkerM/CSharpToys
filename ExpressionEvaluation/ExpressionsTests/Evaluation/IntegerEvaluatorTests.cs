using NUnit.Framework;
using ExpressionsTests.TestEntities;

namespace ExpressionsCore.Evaluation.Tests
{
    [TestFixture]
    public class IntegerEvaluatorTests
    {
        private readonly IntegerEvaluator evaluator = new IntegerEvaluator();

        [Test]
        public void IntegerEvaluationTest()
        {
            foreach (var expressionWrapper in TestExpressionsRepository.TestExpressions)
            {
                int result = expressionWrapper.Expression.AcceptEvaluator(evaluator);
                Assert.AreEqual(expressionWrapper.ExpectedIntegralResult, result);
            }
        }
    }
}