using System;
using System.IO;
using NezarkaModel;
using NezarkaModel.Entities;

namespace NezarkaController
{
    internal class StoreBuilder
    {
        public IStore LoadFrom(TextReader reader)
        {
            var store = new BookStore();

            try
            {
                if (reader.ReadLine() != "DATA-BEGIN")
                    return null;

                while (true)
                {
                    string line = reader.ReadLine();

                    if (line == null)
                        return null;

                    if (line == "DATA-END")
                        break;

                    string[] tokens = line.Split(';');
                    if (!ResolveLine(tokens, store))
                        return null;
                }
            }
            catch (Exception ex) when 
            (ex is FormatException
             || ex is IndexOutOfRangeException)
            {
                return null;
            }

            return store;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="store"></param>
        /// <returns>True if item was succesfully parsed and added to store, False otherwise</returns>
        private static bool ResolveLine(string[] tokens, IStore store)
        {
            switch (tokens[0])
            {
                case "BOOK":
                    store.Add(new Book
                    (
                        id: int.Parse(tokens[1]),
                        title: tokens[2],
                        author: tokens[3],
                        price: decimal.Parse(tokens[4])
                    ));
                    break;
                case "CUSTOMER":
                    store.Add(new Customer
                    (
                        id: int.Parse(tokens[1]),
                        firstName: tokens[2],
                        lastName: tokens[3]
                    ));
                    break;
                case "CART-ITEM":
                    ICustomer customer = store.Customer(int.Parse(tokens[1]));
                    IProduct product = store.Product(int.Parse(tokens[2]));
                    if (customer == null || product == null)
                    {
                        return false;
                    }
                    customer.ShoppingCart.Items.Add(new ShoppingCartItem
                    (
                        product,
                        count: int.Parse(tokens[3])
                    ));
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}
