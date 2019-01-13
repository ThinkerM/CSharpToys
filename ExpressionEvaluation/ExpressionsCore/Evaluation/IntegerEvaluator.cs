using ExpressionsCore.Expressions;
using ExpressionsCore.Expressions.Implementations;

namespace ExpressionsCore.Evaluation
{
    /// <summary>
    /// Evaluates expressions using integer calculation on all values
    /// </summary>
    public sealed class IntegerEvaluator : IEvaluator<int>
    {
        public int Evaluate(Addition addition) 
            => checked(addition.LeftOperand.AcceptEvaluator(this) 
                       + addition.RightOperand.AcceptEvaluator(this));

        public int Evaluate(Subtraction subtraction)
            => checked(subtraction.LeftOperand.AcceptEvaluator(this) 
                       - subtraction.RightOperand.AcceptEvaluator(this));

        public int Evaluate(Multiplication multiplication)
            => checked(multiplication.LeftOperand.AcceptEvaluator(this)
                       * multiplication.RightOperand.AcceptEvaluator(this));

        public int Evaluate(Division division)
            => checked(division.LeftOperand.AcceptEvaluator(this)
                       / division.RightOperand.AcceptEvaluator(this));

        public int Evaluate(Negation negation)
            => checked(-negation.Operand.AcceptEvaluator(this));

        public int Evaluate(ConstantExpression constant)
            => constant.Value;
    }
}
