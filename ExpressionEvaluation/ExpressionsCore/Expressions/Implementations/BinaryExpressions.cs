using ExpressionsCore.Evaluation;
using ExpressionsCore.Operators;

namespace ExpressionsCore.Expressions.Implementations
{
    public sealed class Addition : BinaryExpression
    {
        public Addition(IExpression leftOperand, IExpression rightOperand) : base(leftOperand, rightOperand, Operator.Add) { }

        public override TResult AcceptEvaluator<TResult>(IEvaluator<TResult> evaluator)
            => evaluator.Evaluate(this);
    }

    public sealed class Subtraction : BinaryExpression
    {
        public Subtraction(IExpression leftOperand, IExpression rightOperand) : base(leftOperand, rightOperand, Operator.Subtract) { }

        public override TResult AcceptEvaluator<TResult>(IEvaluator<TResult> evaluator)
            => evaluator.Evaluate(this);
    }

    public sealed class Multiplication : BinaryExpression
    {
        public Multiplication(IExpression leftOperand, IExpression rightOperand) : base(leftOperand, rightOperand, Operator.Multiply) { }

        public override TResult AcceptEvaluator<TResult>(IEvaluator<TResult> evaluator)
            => evaluator.Evaluate(this);
    }

    public sealed class Division : BinaryExpression
    {
        public Division(IExpression leftOperand, IExpression rightOperand) : base(leftOperand, rightOperand, Operator.Divide) { }

        public override TResult AcceptEvaluator<TResult>(IEvaluator<TResult> evaluator)
            => evaluator.Evaluate(this);
    }
}
