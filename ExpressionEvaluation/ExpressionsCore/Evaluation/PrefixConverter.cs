using ExpressionsCore.Expressions;
using ExpressionsCore.Expressions.Implementations;

namespace ExpressionsCore.Evaluation
{
    /// <summary>
    /// Converts an <see cref="IExpression"/> to its prefix (a.k.a. Polish notation) representation (e.g. '+ 1 / 2 3')
    /// </summary>
    public class PrefixConverter : IEvaluator<string>
    {
        public string Evaluate(Addition addition)
            => ConvertBinaryExpression(addition);

        public string Evaluate(Subtraction subtraction)
            => ConvertBinaryExpression(subtraction);

        public string Evaluate(Multiplication multiplication)
            => ConvertBinaryExpression(multiplication);

        public string Evaluate(Division division)
            => ConvertBinaryExpression(division);

        public string Evaluate(Negation negation)
            => ConvertUnaryExpression(negation);

        public string Evaluate(ConstantExpression constant)
            => constant.Value.ToString();

        private string ConvertBinaryExpression(BinaryExpression binaryEx)
        {
            string leftSubExpression = binaryEx.LeftOperand.AcceptEvaluator(this);
            string rightSubExpression = binaryEx.RightOperand.AcceptEvaluator(this);

            return string.Join(" ", leftSubExpression, binaryEx.AssignedOperator.MappedSymbol, rightSubExpression);
        }

        private string ConvertUnaryExpression(UnaryExpression unaryEx)
        {
            string subExpression = unaryEx.Operand.AcceptEvaluator(this);
            return string.Join(" ", unaryEx.AssignedOperator.MappedSymbol, subExpression);
        }
    }
}
