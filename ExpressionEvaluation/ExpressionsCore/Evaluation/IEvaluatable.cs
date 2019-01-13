namespace ExpressionsCore.Evaluation
{
    public interface IEvaluatable
    {
        TResult AcceptEvaluator<TResult>(IEvaluator<TResult> evaluator);
    }
}
