using CommonLibrary;
using ExpressionsCore.Expressions;
using ExpressionsCore.Expressions.Implementations;

namespace ExpressionsCore.Evaluation
{
    /// <summary>
    /// Converts an <see cref="IExpression"/> to an infix representation using the minimal required amount of brackets (e.g. '(1+2)/3*4')
    /// </summary>
    public sealed class InfixMinimalParenthesesConverter : IEvaluator<string>
    {
        public string Evaluate(Addition addition) 
            => AssembleBinaryExpression(addition, 
                leftChildParenthesesCondition: NormalParenthesesCondition, 
                rightChildParenthesesCondition: NormalParenthesesCondition);

        public string Evaluate(Subtraction subtraction)
            => AssembleBinaryExpression(subtraction, 
                leftChildParenthesesCondition: NormalParenthesesCondition, 
                rightChildParenthesesCondition: LeftAssociativeSensitiveCondition);

        public string Evaluate(Multiplication multiplication)
            => AssembleBinaryExpression(multiplication, 
                leftChildParenthesesCondition: NormalParenthesesCondition, 
                rightChildParenthesesCondition: NormalParenthesesCondition);

        public string Evaluate(Division division)
            => AssembleBinaryExpression(division, 
                leftChildParenthesesCondition: NormalParenthesesCondition, 
                rightChildParenthesesCondition: LeftAssociativeSensitiveCondition);

        public string Evaluate(Negation negation)
        {
            string subExpression =
                EvaluateWithParenthesesCondition(negation.Operand, negation, NormalParenthesesCondition);

            return $"-{subExpression}"; //don't use the '~' mapped negation symbol
        }

        public string Evaluate(ConstantExpression constant)
            => constant.Value.ToString();

        /// <summary>
        /// Concatenate the expression's subexpressions while determining proper parentheses usage for the subexpressions
        /// </summary>
        /// <param name="binaryEx">Expression to assemble</param>
        /// <param name="leftChildParenthesesCondition">Decision strategy to be used for determining when the left operand requires parentheses</param>
        /// <param name="rightChildParenthesesCondition">Decision strategy to be used for determining when the right operand requires parentheses</param>
        /// <returns></returns>
        private string AssembleBinaryExpression(
            BinaryExpression binaryEx, 
            SubExpressionParenthesesCondition leftChildParenthesesCondition,
            SubExpressionParenthesesCondition rightChildParenthesesCondition)
        {
            string leftSubExpression =
                EvaluateWithParenthesesCondition(binaryEx.LeftOperand, binaryEx, leftChildParenthesesCondition);

            string rightSubExpression =
                EvaluateWithParenthesesCondition(binaryEx.RightOperand, binaryEx, rightChildParenthesesCondition);

            return string.Concat(leftSubExpression, binaryEx.AssignedOperator.MappedSymbol, rightSubExpression);
        }

        /// <summary>
        /// Call the child expression's representation and surround it with parentheses if the <see cref="parenthesesRequired"/> condition is met
        /// </summary>
        /// <param name="childToEvaluate"></param>
        /// <param name="parentExpression"></param>
        /// <param name="parenthesesRequired"></param>
        /// <returns></returns>
        private string EvaluateWithParenthesesCondition(
            IExpression childToEvaluate,
            IExpression parentExpression,
            SubExpressionParenthesesCondition parenthesesRequired)
        {
            return parenthesesRequired(parentExpression, childToEvaluate)
                ? childToEvaluate.AcceptEvaluator(this).SurroundWithParentheses()
                : childToEvaluate.AcceptEvaluator(this);
        }

        #region Bracket conditions
        /// <summary>
        /// Function determining whether a specific subexpression requires to be surrounded with parentheses given its parent expression
        /// </summary>
        /// <param name="parent">Parent expression of the candidate (child) expression</param>
        /// <param name="child">Candidate expression for being surrounded with parentheses</param>
        /// <returns></returns>
        private delegate bool SubExpressionParenthesesCondition(IExpression parent, IExpression child);

        /// <summary>
        /// Parentheses condition usable for general-case expressions
        /// </summary>
        private static readonly SubExpressionParenthesesCondition NormalParenthesesCondition
            = (parent, child) => parent.Precedence > child.Precedence;

        /// <summary>
        /// Parentheses condition necessary for more tricky left-associative operations where parentheses are required even for same-priority operators
        /// </summary>
        private static readonly SubExpressionParenthesesCondition LeftAssociativeSensitiveCondition
            = (parent, child) => parent.Precedence >= child.Precedence;
        #endregion
    }
}
