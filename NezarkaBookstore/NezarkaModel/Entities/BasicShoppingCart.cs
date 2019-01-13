using System;
using System.Collections.Generic;
using System.Linq;

namespace NezarkaModel.Entities
{
    public class BasicShoppingCart : IShoppingCart
    {
        public ICollection<IShoppingCartItem> Items { get; } = new List<IShoppingCartItem>();

        public void Add(IProduct product, int count = 1)
        {
            IShoppingCartItem itemInCart = Items.FirstOrDefault(item => item.Product == product);
            if (itemInCart != null)
            {
                itemInCart.ChangeAmount(count);
            }
            else
            {
                Items.Add(new ShoppingCartItem(product, count));
            }
        }

        public void Remove(IProduct product, int count = 1)
        {
            IShoppingCartItem itemInCart = Items.FirstOrDefault(item => item.Product == product);
            if (itemInCart == null)
                return;

            itemInCart.ChangeAmount(-count);
            if (itemInCart.Count <= 0)
                Items.Remove(itemInCart);
        }

        public bool ContainsProduct(int productId)
            => Items.Any(item => item.Product.Id == productId);

        [Obsolete("Shopping cart currently doesn't have its own ViewId (is tightly tied to its owner", true)]
        public int ViewId
        {
            get
            {
                throw new NotSupportedException(
                    $"Shopping cart currently doesn't provide its own ViewId, retrieve from the owner instead.");
            }
        }
    }
}