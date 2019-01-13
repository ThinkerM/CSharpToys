using System.IO;
using NezarkaModel;
using NezarkaView.Links;

namespace NezarkaView.Components
{
    public class CustomerMenuView : IView
    {
        private readonly ICustomer user;
        private readonly string booksMenuLink;
        private readonly string cartLink;

        public CustomerMenuView(ICustomer user, ILinkGenerator linkGenerator)
        {
            this.user = user;
            booksMenuLink = linkGenerator.ProductsMenuLink();
            cartLink = linkGenerator.ShoppingCartLink();
        }

        public void GenerateHtml(TextWriter outputTarget, int tabsOffset = 0)
        {
            Output(outputTarget, tabsOffset, "<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>");
            Output(outputTarget, tabsOffset, $"{user.FirstName}, here is your menu:");
            Output(outputTarget, tabsOffset, "<table>");
            Output(outputTarget, tabsOffset, "	<tr>");
            Output(outputTarget, tabsOffset, $"		<td><a href=\"{booksMenuLink}\">Books</a></td>");
            Output(outputTarget, tabsOffset, $"		<td><a href=\"{cartLink}\">Cart ({user.ShoppingCart.Items.Count})</a></td>");
            Output(outputTarget, tabsOffset, "	</tr>");
            Output(outputTarget, tabsOffset, "</table>");
        }

        //Provisionally messy to conform to C#6.0, using a local function inside GenerateHtml becomes nice (no need to pass outputTarget, tabsOffset)
        //Consider moving all of these to a helper/extensions class if this should persist long-term
        private void Output(TextWriter output, int tabsOffset, string line)
            => output.WriteLine(new string('\t', tabsOffset) + line);
    }
}
