using System.Collections.Generic;
using System.IO;
using System.Linq;
using NezarkaCommonLibrary.Extensions;
using NezarkaModel;
using NezarkaView.Links;

namespace NezarkaView.Components.Products
{
    /// <summary>
    /// Holds a series of <see cref="CopyrightableProductListItemView"/>s
    /// </summary>
    internal class CopyrightableProductListView : IView
    {
        private readonly IEnumerable<CopyrightableProductListItemView> listItems;
        private readonly int itemsPerTableLine;

        public CopyrightableProductListView(
            IEnumerable<ICopyrightable> listItems, 
            ILinkGenerator linkGenerator, 
            int itemsPerTableLine)
        {
            this.listItems = listItems.Select(product => new CopyrightableProductListItemView(product, linkGenerator));
            this.itemsPerTableLine = itemsPerTableLine;
        }

        public void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            int tabsOffsetForItems = tabsOffset + 2;

            Output(outputTarget, tabsOffset, "<table>");
            foreach (var itemsBatch in listItems.Batch(itemsPerTableLine))
            {
                Output(outputTarget, tabsOffset, "	<tr>");
                foreach (var item in itemsBatch)
                {
                    item.GenerateHtml(outputTarget, tabsOffsetForItems);
                }
                Output(outputTarget, tabsOffset, "	</tr>");
            }
            Output(outputTarget, tabsOffset, "</table>");
        }

        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
            //Output("...");
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        private void Output(TextWriter output, int tabsOffset, string line)
            => output.WriteLine(new string('\t', tabsOffset) + line);
    }
}
