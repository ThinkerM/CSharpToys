using System.Collections.Generic;

namespace NezarkaModel
{
    public interface IStore
    {
        /// <summary>
        /// Find a product by its ID, null if product not found.
        /// </summary>
        /// <param name="id">ID of the product to search for</param>
        /// <returns></returns>
        IProduct Product(int id);

        /// <summary>
        /// Find a product of a specific type by its ID, null if product not found.
        /// </summary>
        /// <typeparam name="T">Type of the product to search for</typeparam>
        /// <param name="id">ID of the product to search for</param>
        /// <returns></returns>
        /// <remarks>Especially useful if products are only guaranteed to have unique IDs within their own type
        /// but not across all types.</remarks>
        T Product<T>(int id) where T : IProduct;

        /// <summary>
        /// Find all products of a given type
        /// </summary>
        /// <typeparam name="T">Type of the products to filter out</typeparam>
        /// <returns></returns>
        IEnumerable<T> Products<T>() where T : IProduct;

        /// <summary>
        /// Find a customer by their ID.
        /// </summary>
        /// <param name="id">ID of the customer to search for</param>
        /// <returns></returns>
        ICustomer Customer(int id);

        bool HasCustomer(int id);

        bool HasProduct(int id);

        /// <summary>
        /// Register a new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        ICustomer Add(ICustomer customer);

        /// <summary>
        /// Register a new available product type
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        IProduct Add(IProduct product);
    }
}