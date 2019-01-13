using System.IO;

namespace NezarkaView.Components
{
    public class CommandProcessedView : IView
    {
        public void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            outputTarget.NewLine = ViewConstants.NEWLINE;
            outputTarget.WriteLine(ViewConstants.COMMAND_PROCESSED);
        }
    }
}
