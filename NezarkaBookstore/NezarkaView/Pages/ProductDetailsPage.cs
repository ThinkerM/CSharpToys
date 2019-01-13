using System.IO;
using NezarkaModel;
using NezarkaView.Components.Products;
using NezarkaView.Links;

namespace NezarkaView.Pages
{
    /// <summary>
    /// Page containing information about a single <see cref="ICopyrightable"/> + link to purchase it
    /// </summary>
    public class ProductDetailsPage : PageWithGenericHeaderViewBase
    {
        private readonly CopyrightableProductInfoView productInfoView;

        /// <summary>
        /// Page containing information about a single <see cref="ICopyrightable"/> + link to purchase it
        /// </summary>
        
        public ProductDetailsPage(
            ICustomer user,
            ILinkGenerator linkGenerator,
            ICopyrightable product, 
            string productTypeQualification) : base(user, linkGenerator)
        {
            productInfoView =
                new CopyrightableProductInfoView(product, linkGenerator, productTypeQualification);
        }

        protected override void GenerateHtmlBody(TextWriter outputTarget, int tabsOffset = 1)
        {
            productInfoView.GenerateHtml(outputTarget, tabsOffset);
        }
    }
}
