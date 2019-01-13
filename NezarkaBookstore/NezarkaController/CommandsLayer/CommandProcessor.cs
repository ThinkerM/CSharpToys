using System.IO;
using NezarkaView.Components;

namespace NezarkaController.CommandsLayer
{
    internal class CommandProcessor
    {
        public void ProcessCommand(ICommand command, TextWriter output)
        {
            (command as ITransactionCommand)?.PerformTransaction();
            command.GenerateRelatedView(output);
            new CommandProcessedView().GenerateHtml(output);
        }
    }
}
