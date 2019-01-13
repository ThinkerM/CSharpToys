using System.Collections.Generic;
using System.Linq;
using CommonLibrary;
using ExpressionsCore.Expressions;

namespace ExpressionEvaluationService.Parsing
{
    internal class PrefixNotationExpressionParser : IExpressionParser
    {
        //postfix feels easier to evaluate and I'm too lazy to implement prefix when it can be simply converted :D
        private readonly IExpressionParser postfixParser;

        public PrefixNotationExpressionParser(IExpressionParser postfixParser)
        {
            this.postfixParser = postfixParser;
        }

        public IExpression Parse(string expressionString)
        {
            string[] tokens = ParsingHelper.GetTokens(expressionString);
            return postfixParser.Parse(tokens.InPlaceReverse());
        }

        public IExpression Parse(IEnumerable<string> tokens)
        {
            return postfixParser.Parse(tokens.Reverse());
        }

        public bool TryParse(string expressionString, out IExpression resultExpression)
        {
            string[] tokens = ParsingHelper.GetTokens(expressionString);
            return postfixParser.TryParse(tokens.InPlaceReverse(), out resultExpression);
        }

        public bool TryParse(IEnumerable<string> tokens, out IExpression resultExpression)
        {
            return postfixParser.TryParse(tokens.Reverse(), out resultExpression);
        }
    }
}