using System.Collections.Generic;
using System.IO;
using System.Linq;
using NezarkaModel;
using NezarkaView.Links;

namespace NezarkaView.Components.Cart
{
    /// <summary>
    /// Holds multiple <see cref="CartItemView"/>s
    /// </summary>
    internal class CartTableView : IView
    {
        private readonly List<IShoppingCartItem> cartItems;
        private readonly ILinkGenerator linkGenerator;
        private readonly decimal cartItemsTotalPrice;

        public CartTableView(IEnumerable<IShoppingCartItem> cartItems, ILinkGenerator linkGenerator)
        {
            this.cartItems = cartItems.ToList();
            cartItemsTotalPrice = this.cartItems.Sum(item => item.Product.Price * item.Count);
            this.linkGenerator = linkGenerator;
        }

        public void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            int cartItemsOffset = tabsOffset + 1;

            Output(outputTarget, tabsOffset, "<table>");
            Output(outputTarget, tabsOffset, "	<tr>");
            Output(outputTarget, tabsOffset, "		<th>Title</th>");
            Output(outputTarget, tabsOffset, "		<th>Count</th>");
            Output(outputTarget, tabsOffset, "		<th>Price</th>");
            Output(outputTarget, tabsOffset, "		<th>Actions</th>");
            Output(outputTarget, tabsOffset, "	</tr>");
            foreach (var cartItem in cartItems)
            {
                new CartItemView(cartItem, linkGenerator).GenerateHtml(outputTarget, cartItemsOffset);
            }
            Output(outputTarget, tabsOffset, "</table>");
            Output(outputTarget, tabsOffset, $"Total price of all items: {cartItemsTotalPrice} {ViewConstants.CURRENCY}");
        }

        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        void Output(TextWriter outputTarget, int tabsOffset, string line)
            => outputTarget.WriteLine(new string('\t', tabsOffset) + line);
    }
}
