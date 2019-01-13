using System.IO;
using NezarkaModel;
using NezarkaView.Links;

namespace NezarkaView.Components.Cart
{
    /// <summary>
    /// Represents a single <see cref="IShoppingCartItem"/> in a larger list of similar items
    /// </summary>
    internal class CartItemView : IView
    {
        private readonly IShoppingCartItem cartItem;
        private readonly string productDetailsLink;
        private readonly string removeFromCartLink;

        public CartItemView(IShoppingCartItem cartItem, ILinkGenerator linkGenerator)
        {
            this.cartItem = cartItem;
            productDetailsLink = linkGenerator.ProductDetailLink(cartItem.Product.Id);
            removeFromCartLink = linkGenerator.RemoveFromCartLink(cartItem.Product.Id);
        }

        public void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            Output(outputTarget, tabsOffset, "<tr>");
            Output(outputTarget, tabsOffset, $"	<td><a href=\"{productDetailsLink}\">{cartItem.Product.Name}</a></td>");
            Output(outputTarget, tabsOffset, $"	<td>{cartItem.Count}</td>");
            Output(outputTarget, tabsOffset, $"	<td>{GeneratePriceRepresentation()}</td>");
            Output(outputTarget, tabsOffset, $"	<td>&lt;<a href=\"{removeFromCartLink}\">Remove</a>&gt;</td>");
            Output(outputTarget, tabsOffset, "</tr>");
        }

        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
            //Output("...");
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        void Output(TextWriter outputTarget, int tabsOffset, string line)
            => outputTarget.WriteLine(new string('\t', tabsOffset) + line);

        private string GeneratePriceRepresentation()
        {
            int count = cartItem.Count;
            decimal price = cartItem.Product.Price;
            string numbersPart = count > 1
                ? $"{count} * {price} = {count * price}"
                : $"{price}";
            return $"{numbersPart} {ViewConstants.CURRENCY}";
        }
    }
}
