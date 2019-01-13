using System.ComponentModel;
using ExpressionsCore.Expressions;

namespace ExpressionEvaluationService.Parsing
{
    public enum ExpressionNotationType { Prefix, Postfix, Infix }

    public interface IParsingProvider
    {
        bool TryParse(string expressionString, ExpressionNotationType notation, out IExpression resultExpression);
        IExpression Parse(string expressionString, ExpressionNotationType notation);
    }

    public class ParsingProvider : IParsingProvider
    {
        private readonly IParserFactory parserFactory;

        public ParsingProvider(IParserFactory parserFactory)
        {
            this.parserFactory = parserFactory;
        }

        public bool TryParse(string expressionString, ExpressionNotationType notation, out IExpression resultExpression)
        {
            var parser = AssignParser(notation);
            return parser.TryParse(expressionString, out resultExpression);
        }

        public IExpression Parse(string expressionString, ExpressionNotationType notation)
        {
            var parser = AssignParser(notation);
            return parser.Parse(expressionString);
        }

        private IExpressionParser AssignParser(ExpressionNotationType notation)
        {
            switch (notation)
            {
                case ExpressionNotationType.Prefix:
                    return parserFactory.CreatePrefixParser();
                case ExpressionNotationType.Postfix:
                    return parserFactory.CreatePostfixParser();
                case ExpressionNotationType.Infix:
                    return parserFactory.CreateInfixParser();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
