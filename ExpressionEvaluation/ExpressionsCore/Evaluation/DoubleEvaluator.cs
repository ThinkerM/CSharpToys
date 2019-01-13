using System;
using ExpressionsCore.Expressions;
using ExpressionsCore.Expressions.Implementations;

namespace ExpressionsCore.Evaluation
{
    /// <summary>
    /// Evaluates expressions using real-number computing on all values
    /// </summary>
    public sealed class DoubleEvaluator : IEvaluator<double>
    {
        public double Evaluate(Addition addition)
            => EvaluateBinary(addition, (x, y) => x + y);

        public double Evaluate(Subtraction subtraction)
            => EvaluateBinary(subtraction, (x, y) => x - y);

        public double Evaluate(Multiplication multiplication)
            => EvaluateBinary(multiplication, (x, y) => x * y);

        public double Evaluate(Division division)
            => EvaluateBinary(division, (x, y) => x / y);

        public double Evaluate(Negation negation)
            => EvaluateUnary(negation, x => -x);

        public double Evaluate(ConstantExpression constant)
            => constant.Value;

        private double EvaluateBinary(BinaryExpression binaryEx, Func<double, double, double> binaryFunc)
            => binaryFunc(
                binaryEx.LeftOperand.AcceptEvaluator(this), 
                binaryEx.RightOperand.AcceptEvaluator(this));

        private double EvaluateUnary(UnaryExpression unaryEx, Func<double, double> unaryFunc)
            => unaryFunc(unaryEx.Operand.AcceptEvaluator(this));
    }
}
