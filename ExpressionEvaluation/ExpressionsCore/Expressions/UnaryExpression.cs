using ExpressionsCore.Operators;

#pragma warning disable 618

namespace ExpressionsCore.Expressions
{
    public abstract class UnaryExpression : CompoundExpression
    {
        public IExpression Operand { get; }

        protected UnaryExpression(IExpression operand, Operator @operator)
            : base(@operator)
        {
            this.Operand = operand;
        }
    }
}
