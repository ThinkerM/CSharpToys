using System.IO;
using NezarkaModel;
using NezarkaView.Links;

namespace NezarkaView.Components.Products
{
    /// <summary>
    /// Represents a single <see cref="ICopyrightable"/> as a component of a larger list of several such items
    /// </summary>
    internal class CopyrightableProductListItemView : CopyrightableProductViewBase
    {
        protected readonly string ProductDetailsLink;

        public CopyrightableProductListItemView(
            ICopyrightable product, 
            ILinkGenerator linkGenerator) : base(product, linkGenerator)
        {
            ProductDetailsLink = linkGenerator.ProductDetailLink(product.Id);
        }

        public override void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            Output(outputTarget, tabsOffset, "<td style=\"padding: 10px;\">");
            Output(outputTarget, tabsOffset + 1, $"<a href=\"{ProductDetailsLink}\">{this.Title}</a><br />");
            Output(outputTarget, tabsOffset + 1, $"Author: {this.Author}<br />");
            Output(outputTarget, tabsOffset + 1, $"Price: {this.Price} {ViewConstants.CURRENCY} &lt;<a href=\"{this.BuyProductLink}\">Buy</a>&gt;");
            Output(outputTarget, tabsOffset, "</td>");
        }
        
        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
            //Output("...");
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        private void Output(TextWriter output, int tabsOffset, string line)
            => output.WriteLine(new string('\t', tabsOffset) + line);
    }
}
