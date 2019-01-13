using System.IO;
using NezarkaView.Components;

namespace NezarkaView.Pages
{
    public class InvalidRequestPage : PageViewBase
    {
        public InvalidRequestPage()
        {
            Header = new EmptyHeader();
        }

        protected override void GenerateHtmlBody(TextWriter outputTarget, int tabsOffset = 1)
        {
            outputTarget.WriteLine(new string('\t', tabsOffset) + "<p>Invalid request.</p>");
        }

        protected override IView Header { get; }
    }
}
