using ExpressionsCore.Evaluation;
using ExpressionsCore.Operators;

namespace ExpressionsCore.Expressions
{
    public abstract class CompoundExpression : IExpression
    {
        protected CompoundExpression(Operator assignedOperator)
        {
            AssignedOperator = assignedOperator;
        }
        public Operator AssignedOperator { get; }
        public abstract TResult AcceptEvaluator<TResult>(IEvaluator<TResult> evaluator);
        public int Precedence => AssignedOperator.Precedence;
    }
}
