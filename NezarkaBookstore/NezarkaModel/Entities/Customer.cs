namespace NezarkaModel.Entities
{
    public class Customer : ICustomer
    {
        public Customer(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }

        private IShoppingCart shoppingCart;
        public IShoppingCart ShoppingCart 
            => shoppingCart ?? (shoppingCart = new BasicShoppingCart());
    }
}