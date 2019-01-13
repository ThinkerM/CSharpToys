using System.IO;

namespace NezarkaView.Components
{
    public class NezarkaPageStyleSetup : IView
    {
        public void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            Output(outputTarget, tabsOffset, "<style type=\"text/css\">");
            Output(outputTarget, tabsOffset, "	table, th, td {");
            Output(outputTarget, tabsOffset, "		border: 1px solid black;");
            Output(outputTarget, tabsOffset, "		border-collapse: collapse;");
            Output(outputTarget, tabsOffset, "	}");
            Output(outputTarget, tabsOffset, "	table {");
            Output(outputTarget, tabsOffset, "		margin-bottom: 10px;");
            Output(outputTarget, tabsOffset, "	}");
            Output(outputTarget, tabsOffset, "	pre {");
            Output(outputTarget, tabsOffset, "		line-height: 70%;");
            Output(outputTarget, tabsOffset, "	}");
            Output(outputTarget, tabsOffset, "</style>");
        }

        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        private void Output(TextWriter output, int tabsOffset, string line)
            => output.WriteLine(new string('\t', tabsOffset) + line);
    }
}
