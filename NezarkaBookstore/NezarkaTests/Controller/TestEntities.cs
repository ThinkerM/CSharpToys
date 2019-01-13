using System.Collections.Generic;
using System.Linq;
using NezarkaModel;
using NezarkaModel.Entities;

namespace NezarkaTests.Controller
{
    internal class TestStore : IStore
    {
        private readonly ICollection<IProduct> products = new List<IProduct>
        {
            new Book(1, "Book1", "Author1", 1),
            new Book(2, "Book2", "Author2", 2),
            new Book(3, "Book3", "Author3", 3),
            new Book(4, "Book4", "Author4", 4),
            new Book(5, "Book5", "Author5", 5)
        };

        private readonly ICollection<ICustomer> customers = new List<ICustomer>
        {
            new Customer(1, "Guy1", "Smith"),
            new Customer(2, "Guy2", "Smith"),
            new Customer(3, "Guy3", "Smith"),
            new Customer(4, "Guy4", "Smith")
        };

        public IProduct Product(int id)
        {
            return products.FirstOrDefault(book => book.Id == id)
                   ?? new Book(-1, "FakeBook", "FakeDude", 0);
        }

        public T Product<T>(int id)
            where T : IProduct
        {
            return Products<T>().FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<T> Products<T>()
            where T : IProduct
        {
            return products.OfType<T>();
        }

        public ICustomer Customer(int id)
        {
            return customers.FirstOrDefault(customer => customer.Id == id)
                   ?? new Customer(-1, "FakeCustomer", "");
        }

        public bool HasCustomer(int id)
        {
            return customers.Any(customer => customer.Id == id);
        }

        public bool HasProduct(int id)
        {
            return products.Any(product => product.Id == id);
        }

        public ICustomer Add(ICustomer customer)
        {
            customers.Add(customer);
            return customer;
        }

        public IProduct Add(IProduct product)
        {
            products.Add(product);
            return product;
        }
    }
}
