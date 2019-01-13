using System;
using System.Collections.Generic;
using System.IO;
using ExpressionsCore.Evaluation;
using ExpressionsCore.Expressions;

namespace ExpressionEvaluationService.Evaluation
{
    public enum EvaluationStrategy { Integer, Real, InfixFullParentheses, InfixMinimalParentheses }

    internal class EvaluationProvider
    {
        private readonly IDictionary<EvaluationStrategy, EvaluationAction> evaluationsMap;

        public EvaluationProvider()
        {
            evaluationsMap = new Dictionary<EvaluationStrategy, EvaluationAction>
            {
                { EvaluationStrategy.Integer, evaluateIntegerExpression },
                { EvaluationStrategy.Real, evaluateRealExpression },
                { EvaluationStrategy.InfixFullParentheses, convertToInfixUnreduced },
                { EvaluationStrategy.InfixMinimalParentheses, convertToInfixReduced }
            };
        }

        private delegate void EvaluationAction(IExpression expression, TextWriter output);

        public void Evaluate(IExpression expression, EvaluationStrategy evaluationStrategy, TextWriter output)
        {
            EvaluationAction evaluation;
            if (evaluationsMap.TryGetValue(evaluationStrategy, out evaluation))
            {
                evaluation(expression, output);
            }
            else
            {
                throw new ArgumentException("No evaluation method found for the provided strategy.", nameof(evaluationStrategy));
            }
        }

        private readonly EvaluationAction evaluateIntegerExpression = (expression, output) =>
        {
            try
            {
                StandardEvaluation(new IntegerEvaluator(), expression, output);
            }
            catch (DivideByZeroException)
            {
                output.WriteLine(EvaluationConstants.DIVISION_ERROR);
            }
            catch (OverflowException)
            {
                output.WriteLine(EvaluationConstants.OVERFLOW_ERROR);
            }
        };

        private readonly EvaluationAction evaluateRealExpression = (expression, output) =>
        {
            double result = expression.AcceptEvaluator(new DoubleEvaluator());
            output.WriteLine(result.ToString("F5"));
        };

        private readonly EvaluationAction convertToInfixUnreduced = (expression, output)
            => StandardEvaluation(new InfixFullParenthesesConverter(), expression, output);

        private readonly EvaluationAction convertToInfixReduced = (expression, output)
            => StandardEvaluation(new InfixMinimalParenthesesConverter(), expression, output);

        private static void StandardEvaluation<TResult>(IEvaluator<TResult> evaluator, IExpression expression, TextWriter output)
        {
            TResult result = expression.AcceptEvaluator(evaluator);
            output.WriteLine(result);
        }
    }
}
