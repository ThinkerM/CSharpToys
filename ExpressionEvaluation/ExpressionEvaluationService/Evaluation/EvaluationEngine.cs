using System.IO;
using ExpressionEvaluationService.Parsing;
using ExpressionsCore.Expressions;

namespace ExpressionEvaluationService.Evaluation
{
    public class EvaluationEngine
    {
        public IExpression LastValidExpression { get; private set; }

        private EvaluationProvider evaluator = new EvaluationProvider();

        public void InsertExpression(string expression, ExpressionNotationType notation, TextWriter feedbackOutput)
        {
            var parser = new ParsingProvider(new ParserFactory());
            IExpression parsedExpression;

            if (parser.TryParse(expression, notation, out parsedExpression))
            {
                LastValidExpression = parsedExpression;
            }
            else
            {
                feedbackOutput.WriteLine(ParsingConstants.FORMAT_ERROR);
                LastValidExpression = null;
            }
        }

        private const string MISSING_EXPRESSION = "Expression Missing";

        public void EvaluateLastExpression(EvaluationStrategy evaluationStrategy, TextWriter output)
        {
            EvaluateExpression(LastValidExpression, evaluationStrategy, output);
        }

        public void EvaluateExpression(IExpression expression, EvaluationStrategy evaluationStrategy, TextWriter output)
        {
            if (expression == null)
            {
                output.WriteLine(MISSING_EXPRESSION);
                return;
            }

            evaluator.Evaluate(expression, evaluationStrategy, output);
            LastValidExpression = expression;
        }
    }
}
