using ExpressionsCore.Expressions;
using ExpressionsCore.Expressions.Implementations;

namespace ExpressionsCore.Evaluation
{
    public interface IEvaluator<out TResult>
    {
        TResult Evaluate(Addition addition);
        TResult Evaluate(Subtraction subtraction);
        TResult Evaluate(Multiplication multiplication);
        TResult Evaluate(Division division);
        TResult Evaluate(Negation negation);
        TResult Evaluate(ConstantExpression constant);
    }
}
