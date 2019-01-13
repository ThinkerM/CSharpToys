namespace NezarkaView.Links
{
    public interface ILinkGenerator
    {
        string ProductsMenuLink();
        string ProductDetailLink(int productId);
        string ShoppingCartLink();
        string AddToCartLink(int productId);
        string RemoveFromCartLink(int productId);
    }
}
