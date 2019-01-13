using System.Collections.Generic;

namespace NezarkaModel
{
    public interface IShoppingCart : IViewable
    {
        ICollection<IShoppingCartItem> Items { get; }

        void Add(IProduct product, int count = 1);

        void Remove(IProduct product, int count = 1);

        bool ContainsProduct(int productId);
    }
}