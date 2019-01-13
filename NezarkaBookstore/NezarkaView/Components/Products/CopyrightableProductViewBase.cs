using System.IO;
using NezarkaModel;
using NezarkaView.Links;

namespace NezarkaView.Components.Products
{
    internal abstract class CopyrightableProductViewBase : IView
    {
        protected readonly string Author;
        protected readonly string Title;
        protected readonly decimal Price;
        protected readonly string BuyProductLink;

        protected CopyrightableProductViewBase(ICopyrightable product, ILinkGenerator linkGenerator)
        {
            Author = product.Author;
            Title = product.Title;
            Price = product.Price;
            this.BuyProductLink = linkGenerator.AddToCartLink(product.Id);
        }

        public abstract void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0);
    }
}
