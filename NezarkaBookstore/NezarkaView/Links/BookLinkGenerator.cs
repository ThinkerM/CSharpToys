namespace NezarkaView.Links
{
    public class BookLinkGenerator : ILinkGenerator
    {
        public string ProductsMenuLink()
            => ConstructLink("Books");

        public string ProductDetailLink(int productId)
            => ConstructLink("Books", "Detail", productId.ToString());

        public string ShoppingCartLink()
            => ConstructLink("ShoppingCart");

        public string AddToCartLink(int productId)
            => ConstructLink("ShoppingCart", "Add", productId.ToString());

        public string RemoveFromCartLink(int productId)
            => ConstructLink("ShoppingCart", "Remove", productId.ToString());

        private static string ConstructLink(params string[] uniqueSegments) 
            => "/" + LinkingHelper.ConstructPath(uniqueSegments);
    }
}
