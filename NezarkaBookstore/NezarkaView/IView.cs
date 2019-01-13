using System.IO;

namespace NezarkaView
{
    public interface IView
    {
        void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0);
    }
}
