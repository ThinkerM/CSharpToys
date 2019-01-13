using System;
using System.Collections.Generic;
using CommonLibrary;
using ExpressionsCore.Expressions;
using ExpressionsCore.Operators;

namespace ExpressionEvaluationService.Parsing
{
    internal class PostfixNotationExpressionParser : IExpressionParser
    {
        private readonly IExpressionFactory expressionFactory;

        public PostfixNotationExpressionParser(IExpressionFactory expressionFactory)
        {
            this.expressionFactory = expressionFactory;
        }

        public IExpression Parse(string expressionString)
        {
            string[] tokens = ParsingHelper.GetTokens(expressionString);
            return this.Parse(tokens);
        }

        public IExpression Parse(IEnumerable<string> tokens)
        {
            return ParseUnsafe(tokens);
        }

        public bool TryParse(string expressionString, out IExpression resultExpression)
        {
            string[] tokens = ParsingHelper.GetTokens(expressionString);
            return this.TryParse(tokens, out resultExpression);
        }

        public bool TryParse(IEnumerable<string> tokens, out IExpression resultExpression)
        {
            try
            {
                resultExpression = ParseUnsafe(tokens);
                return true;
            }
            catch (FormatException)
            {
                resultExpression = null;
                return false;
            }
        }

        /// <summary>
        /// Parse an expression or throw a <see cref="FormatException"/> if the expression isn't in a valid format 
        /// (amount of operands and operators doesn't match, wrong order, invalid tokens, ...)
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        private IExpression ParseUnsafe(IEnumerable<string> tokens)
        {
            var expressionStack = new Stack<IExpression>();
            foreach (string token in tokens)
            {
                int operandValue;
                if (IsOperator(token))
                {
                    Operator @operator = Operator.GetMapped(token[0]);
                    var operands = PopRequiredOperands(expressionStack, @operator);
                    var operation = expressionFactory.CreateExpression(@operator, operands);
                    expressionStack.Push(operation);
                }
                else if (int.TryParse(token, out operandValue))
                {
                    expressionStack.Push(expressionFactory.CreateExpression(operandValue));
                }
                else
                {
                    throw new FormatException($"Token {token} could not be parsed as an operator nor a numeric value.");
                }
            }
            if (expressionStack.Count != 1)
                throw new FormatException("The expression didn't have a matching amount of operands and operators.");

            return expressionStack.Pop(); //last expression remaining on the stack is the final result
        }

        private static bool IsOperator(string s)
            => s.Length == 1 && Operator.IsDefined(s[0]);

        private static IEnumerable<IExpression> PopRequiredOperands(Stack<IExpression> stack, Operator op)
        {
            if (stack.Count < op.RequiredOperandsCount)
                throw new FormatException("The expression didn't have a sufficient amount of operands for the contained operators.");

            return stack.PopMultiple(op.RequiredOperandsCount);
        }
    }
}
