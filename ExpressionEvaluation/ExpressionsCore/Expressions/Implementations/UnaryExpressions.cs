using ExpressionsCore.Evaluation;
using ExpressionsCore.Operators;

namespace ExpressionsCore.Expressions.Implementations
{
    public sealed class Negation : UnaryExpression
    {
        public Negation(IExpression operand) : base(operand, Operator.Negate) { }

        public override TResult AcceptEvaluator<TResult>(IEvaluator<TResult> evaluator)
            => evaluator.Evaluate(this);
    }
}
