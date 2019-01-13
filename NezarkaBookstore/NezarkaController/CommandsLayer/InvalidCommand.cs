using System.IO;
using NezarkaView.Pages;

namespace NezarkaController.CommandsLayer
{
    internal class InvalidCommand : ICommand
    {
        public void GenerateRelatedView(TextWriter outputTarget)
        {
            new InvalidRequestPage().GenerateHtml(outputTarget);
        }
    }
}
