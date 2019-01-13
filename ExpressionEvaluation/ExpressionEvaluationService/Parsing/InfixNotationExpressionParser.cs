using System;
using System.Collections.Generic;
using ExpressionsCore.Expressions;

namespace ExpressionEvaluationService.Parsing
{
    internal class InfixNotationExpressionParser : IExpressionParser
    {
        public IExpression Parse(string expressionString)
        {
            throw new NotImplementedException();
        }

        public IExpression Parse(IEnumerable<string> tokens)
        {
            throw new NotImplementedException();
        }

        public bool TryParse(string expressionString, out IExpression resultExpression)
        {
            throw new NotImplementedException();
        }

        public bool TryParse(IEnumerable<string> tokens, out IExpression resultExpression)
        {
            throw new NotImplementedException();
        }
    }
}
