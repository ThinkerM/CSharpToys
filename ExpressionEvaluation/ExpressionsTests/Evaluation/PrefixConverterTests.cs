using NUnit.Framework;
using ExpressionEvaluationService.Parsing;
using ExpressionsCore.Expressions;

namespace ExpressionsCore.Evaluation.Tests
{
    [TestFixture]
    public class PrefixConverterTests
    {
        private readonly ParsingProvider parser = new ParsingProvider(new ParserFactory());
        private readonly PrefixConverter converter = new PrefixConverter();

        [Test]
        [TestCase("+ 10 3")]
        [TestCase("- 10 3")]
        [TestCase("* 10 3")]
        [TestCase("/ 10 3")]
        [TestCase("~ 1")]
        [TestCase("/ 10 + 3 * 1 2")]
        [TestCase("* / + 1 2 3 4")]
        public void ConversionTest(string prefixInput)
        {
            if (!parser.TryParse(prefixInput, ExpressionNotationType.Prefix, out IExpression parsedExpression))
                Assert.Inconclusive(); //invalid format, nothing to test

            string conversionRoundtrip = parsedExpression.AcceptEvaluator(converter);
            Assert.AreEqual(prefixInput, conversionRoundtrip);
        }
    }
}