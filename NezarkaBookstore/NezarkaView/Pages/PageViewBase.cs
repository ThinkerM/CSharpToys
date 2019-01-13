using System.IO;

namespace NezarkaView.Pages
{
    public abstract class PageViewBase : IView
    {
        protected abstract void GenerateHtmlBody(TextWriter outputTarget, int tabsOffset = 1);

        protected abstract IView Header { get; }

        protected virtual int HtmlBodyOffset => 0;

        public void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            outputTarget.NewLine = ViewConstants.NEWLINE;

            Output(outputTarget, tabsOffset, "<!DOCTYPE html>");
            Output(outputTarget, tabsOffset, "<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            Output(outputTarget, tabsOffset, "<head>");
            Output(outputTarget, tabsOffset, "	<meta charset=\"utf-8\" />");
            Output(outputTarget, tabsOffset, "	<title>Nezarka.net: Online Shopping for Books</title>");
            Output(outputTarget, tabsOffset, "</head>");
            Output(outputTarget, tabsOffset, "<body>");

            Header.GenerateHtml(outputTarget, tabsOffset);
            GenerateHtmlBody(outputTarget, HtmlBodyOffset);

            Output(outputTarget, tabsOffset, "</body>");
            Output(outputTarget, tabsOffset, "</html>");
        }

        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
            //Output("...");
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        void Output(TextWriter outputTarget, int tabsOffset, string line)
            => outputTarget.WriteLine(new string('\t', tabsOffset) + line);
    }
}
