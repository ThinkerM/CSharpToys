using ExpressionsCore.Expressions;

namespace ExpressionEvaluationService.Parsing
{
    public interface IParserFactory
    {
        IExpressionParser CreatePrefixParser();
        IExpressionParser CreatePostfixParser();
        IExpressionParser CreateInfixParser();
    }

    public class ParserFactory : IParserFactory
    {
        public IExpressionParser CreatePrefixParser()
            => new PrefixNotationExpressionParser(CreatePostfixParser());

        public IExpressionParser CreatePostfixParser()
            => new PostfixNotationExpressionParser(new ExpressionFactory());

        public IExpressionParser CreateInfixParser()
            => new InfixNotationExpressionParser();
    }
}
