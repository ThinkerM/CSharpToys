using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NezarkaView.Components
{
    internal class NezarkaCommonHeaderView : IView
    {
        private readonly IView pageSetup;
        private readonly IView menu;

        public NezarkaCommonHeaderView(IView pageSetup, IView menu)
        {
            this.pageSetup = pageSetup;
            this.menu = menu;
        }

        public void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            pageSetup.GenerateHtml(outputTarget, tabsOffset);
            menu.GenerateHtml(outputTarget, tabsOffset);
        }
    }
}
