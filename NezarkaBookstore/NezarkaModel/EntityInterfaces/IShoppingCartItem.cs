namespace NezarkaModel
{
    public interface IShoppingCartItem
    {
        /// <summary>
        /// The actual item contained in the cart
        /// </summary>
        IProduct Product { get; }

        /// <summary>
        /// Amount of products of the given type contained in the cart 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Increase/decrease the amount of items logged for the product in the cart.
        /// If a change in amount would result in negative count, count will be kept at 0.
        /// </summary>
        /// <param name="n"></param>
        void ChangeAmount(int n);
    }
}