namespace NezarkaController.CommandsLayer
{
    /// <summary>
    /// Represents a command which performs a buy/sell/undo transaction and generates an appropriate view
    /// </summary>
    internal interface ITransactionCommand : ICommand
    {
        void PerformTransaction();
    }
}
