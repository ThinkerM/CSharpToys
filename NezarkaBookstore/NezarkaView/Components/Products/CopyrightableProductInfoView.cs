using System.IO;
using NezarkaCommonLibrary.Extensions;
using NezarkaModel;
using NezarkaView.Links;

namespace NezarkaView.Components.Products
{
    /// <summary>
    /// Represents more detailed information about a single <see cref="ICopyrightable"/>
    /// </summary>
    internal class CopyrightableProductInfoView : CopyrightableProductViewBase
    {
        protected readonly string ProductTypeQualification;

        public CopyrightableProductInfoView(
            ICopyrightable product, 
            ILinkGenerator linkGenerator,
            string productTypeQualification) : base(product, linkGenerator)
        {
            ProductTypeQualification = productTypeQualification;
        }

        public override void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            Output(outputTarget, tabsOffset, $"{ProductTypeQualification.Capitalize()} details:");
            Output(outputTarget, tabsOffset, $"<h2>{this.Title}</h2>");
            Output(outputTarget, tabsOffset, "<p style=\"margin-left: 20px\">");
            Output(outputTarget, tabsOffset, $"Author: {this.Author}<br />");
            Output(outputTarget, tabsOffset, $"Price: {this.Price} {ViewConstants.CURRENCY}<br />");
            Output(outputTarget, tabsOffset, "</p>");
            Output(outputTarget, tabsOffset, $"<h3>&lt;<a href=\"{this.BuyProductLink}\">Buy this {ProductTypeQualification.Decapitalize()}</a>&gt;</h3>");
        }

        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
            //Output("...");
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        private void Output(TextWriter output, int tabsOffset, string line)
            => output.WriteLine(new string('\t', tabsOffset) + line);
    }
}
