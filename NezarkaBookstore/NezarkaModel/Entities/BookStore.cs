using System;
using System.Collections.Generic;
using System.Linq;

namespace NezarkaModel.Entities
{
    public class BookStore : IStore
    {
        private readonly ICollection<ICustomer> customers = new List<ICustomer>();

        private readonly ICollection<IProduct> products = new List<IProduct>();

        public IProduct Product(int id)
            => products.FirstOrDefault(p => p.Id == id);

        public T Product<T>(int id)
            where T : IProduct
            => Products<T>().FirstOrDefault(p => p.Id == id);

        public IEnumerable<T> Products<T>()
            where  T : IProduct
            => products.OfType<T>();

        public ICustomer Customer(int id) 
            => customers.FirstOrDefault(c => c.Id == id);

        public bool HasCustomer(int id)
            => customers.Any(c => c.Id == id);

        public bool HasProduct(int id)
            => products.Any(p => p.Id == id);

        public ICustomer Add(ICustomer customer)
        {
            if (customers.Contains(customer))
                throw new ArgumentException("Trying to add a customer duplicate!", nameof(customer));

            customers.Add(customer);
            return customer;
        }

        public IProduct Add(IProduct product)
        {
            if (products.Contains(product))
                throw new ArgumentException("Trying to add a product duplicate!", nameof(product));

            products.Add(product);
            return product;
        }
    }
}