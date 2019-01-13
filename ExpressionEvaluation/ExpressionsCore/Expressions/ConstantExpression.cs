using ExpressionsCore.Evaluation;

namespace ExpressionsCore.Expressions
{
    public sealed class ConstantExpression : IExpression
    {
        public int Value { get; }

        public ConstantExpression(int value)
        {
            this.Value = value;
        }

        public int Precedence => int.MaxValue;

        public TResult AcceptEvaluator<TResult>(IEvaluator<TResult> evaluator) 
            => evaluator.Evaluate(this);
    }
}
