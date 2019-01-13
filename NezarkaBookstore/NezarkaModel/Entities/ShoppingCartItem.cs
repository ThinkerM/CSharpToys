using System;

namespace NezarkaModel.Entities
{
    public class ShoppingCartItem : IShoppingCartItem
    {
        public ShoppingCartItem(IProduct product, int count = 1)
        {
            Product = product;
            Count = count;
        }

        public IProduct Product { get; }

        public int Count { get; private set; }

        public void ChangeAmount(int n) 
            => Count = Math.Max(0, Count += n);
    }
}