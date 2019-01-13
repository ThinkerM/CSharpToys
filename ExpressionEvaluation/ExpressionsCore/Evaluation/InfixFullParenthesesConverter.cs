using ExpressionsCore.Expressions;
using ExpressionsCore.Expressions.Implementations;
using CommonLibrary;

namespace ExpressionsCore.Evaluation
{
    /// <summary>
    /// Converts an <see cref="IExpression"/> to its full-bracketed infix representation (e.g. '(1+(2/3))' )
    /// </summary>
    public sealed class InfixFullParenthesesConverter : IEvaluator<string>
    {
        public string Evaluate(Addition addition) 
            => ConvertBinaryExpression(addition).SurroundWithParentheses();

        public string Evaluate(Subtraction subtraction)
            => ConvertBinaryExpression(subtraction).SurroundWithParentheses();

        public string Evaluate(Multiplication multiplication)
            => ConvertBinaryExpression(multiplication).SurroundWithParentheses();

        public string Evaluate(Division division)
            => ConvertBinaryExpression(division).SurroundWithParentheses();

        public string Evaluate(Negation negation)
            => $"-{negation.Operand.AcceptEvaluator(this)}".SurroundWithParentheses(); //don't use the '~' mapped symbol for negation

        public string Evaluate(ConstantExpression constant)
            => constant.Value.ToString();

        private string ConvertBinaryExpression(BinaryExpression binaryExpression)
            => string.Concat(
                binaryExpression.LeftOperand.AcceptEvaluator(this),
                binaryExpression.AssignedOperator.MappedSymbol,
                binaryExpression.RightOperand.AcceptEvaluator(this));
    }
}
