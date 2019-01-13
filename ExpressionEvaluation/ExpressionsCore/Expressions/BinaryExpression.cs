using ExpressionsCore.Operators;

#pragma warning disable 618

namespace ExpressionsCore.Expressions
{
    public abstract class BinaryExpression : CompoundExpression
    {
        public IExpression LeftOperand { get; }
        public IExpression RightOperand { get; }

        protected BinaryExpression(IExpression leftOperand, IExpression rightOperand, Operator @operator)
            : base(@operator)
        {
            this.LeftOperand = leftOperand;
            this.RightOperand = rightOperand;
        }
    }
}
