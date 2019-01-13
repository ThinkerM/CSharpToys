using System.IO;
using NezarkaModel;
using NezarkaView.Components.Cart;
using NezarkaView.Links;

namespace NezarkaView.Pages
{
    /// <summary>
    /// A view of an <see cref="IShoppingCart"/> with the contained items and their prices listed out.
    /// </summary>
    public class ShoppingCartPage : PageWithGenericHeaderViewBase
    {
        private readonly CartTableView cartTable;
        private readonly bool cartEmpty;

        /// <summary>
        /// A view of an <see cref="IShoppingCart"/> with the contained items and their prices listed out.
        /// </summary>
        public ShoppingCartPage(ICustomer user, ILinkGenerator linkGenerator) : base(user, linkGenerator)
        {
            cartEmpty = user.ShoppingCart.Items.Count == 0;
            cartTable = new CartTableView(user.ShoppingCart.Items, linkGenerator);
        }

        protected override void GenerateHtmlBody(TextWriter outputTarget, int tabsOffset = 1)
        {
            if (cartEmpty)
            {
                Output(outputTarget, tabsOffset,"Your shopping cart is EMPTY.");
                return;
            }

            Output(outputTarget, tabsOffset,"Your shopping cart:");
            cartTable.GenerateHtml(outputTarget, tabsOffset);
        }

        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
            //Output("...");
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        void Output(TextWriter outputTarget, int tabsOffset, string line)
            => outputTarget.WriteLine(new string('\t', tabsOffset) + line);
    }
}
