using System.Collections.Generic;
using System.IO;
using NezarkaCommonLibrary.Extensions;
using NezarkaModel;
using NezarkaView.Components.Products;
using NezarkaView.Links;

namespace NezarkaView.Pages
{
    /// <summary>
    /// Represents a page with a "menu" of available products
    /// </summary>
    public class ItemsOfferPage : PageWithGenericHeaderViewBase
    {
        private readonly string productsTypeQualification;
        private readonly CopyrightableProductListView productListView;

        /// <summary>
        /// Represents a page with a "menu" of available products
        /// </summary>
        public ItemsOfferPage(
            ICustomer user, 
            ILinkGenerator linkGenerator,
            IEnumerable<ICopyrightable> productsToDisplay, 
            int itemsPerTableLine, 
            string pluralProductTypeQualification) : base(user, linkGenerator)
        {
            productsTypeQualification = pluralProductTypeQualification;
            productListView = new CopyrightableProductListView(productsToDisplay, linkGenerator, itemsPerTableLine);
        }

        protected override void GenerateHtmlBody(TextWriter outputTarget, int tabsOffset = 1)
        {
            Output(outputTarget, tabsOffset, $"Our {productsTypeQualification.Decapitalize()} for you:");
            productListView.GenerateHtml(outputTarget, tabsOffset);
        }

        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
            //Output("...");
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        void Output(TextWriter outputTarget, int tabsOffset, string line)
            => outputTarget.WriteLine(new string('\t', tabsOffset) + line);
    }
}
