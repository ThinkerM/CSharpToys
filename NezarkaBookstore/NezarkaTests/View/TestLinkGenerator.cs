using NezarkaView.Links;

namespace NezarkaTests.View
{
    internal class TestLinkGenerator : ILinkGenerator
    {
        public string ProductsMenuLink() => "/Books";

        public string ProductDetailLink(int productId) => $"/Books/Detail/{productId}";

        public string ShoppingCartLink() => "/ShoppingCart";

        public string AddToCartLink(int productId) => $"/ShoppingCart/Add/{productId}";

        public string RemoveFromCartLink(int productId) => $"/ShoppingCart/Remove/{productId}";
    }
}
