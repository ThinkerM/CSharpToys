using ExpressionsCore.Evaluation;

namespace ExpressionsCore.Expressions
{
    public interface IExpression : IEvaluatable
    {
        int Precedence { get; }
    }
}
