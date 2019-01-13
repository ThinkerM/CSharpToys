using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NezarkaView.Components
{
    internal class EmptyHeader : IView
    {
        public void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            //generate nothing
        }
    }
}
