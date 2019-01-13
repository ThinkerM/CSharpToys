namespace NezarkaModel
{
    public interface ICustomer : IPerson
    {
        IShoppingCart ShoppingCart { get; }
    }
}