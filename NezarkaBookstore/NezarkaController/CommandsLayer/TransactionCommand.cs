using System;
using System.ComponentModel;
using System.IO;
using NezarkaModel;
using NezarkaView;
using NezarkaView.Links;
using NezarkaView.Pages;

namespace NezarkaController.CommandsLayer
{
    internal class TransactionCommand : ITransactionCommand
    {
        public enum CartAction { Add, Remove }

        public TransactionCommand(IStore store, int userId, int productId, int count, CartAction actionType)
        {
            productCount = count;
            underlyingProduct = store.Product(productId);
            underlyingUser = store.Customer(userId);

            switch (actionType)
            {
                case CartAction.Add:
                    transaction = underlyingUser.ShoppingCart.Add;
                    break;
                case CartAction.Remove:
                    transaction = underlyingUser.ShoppingCart.Remove;
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Unexpected {nameof(actionType)}");
            }
        }

        private readonly ICustomer underlyingUser;
        private readonly IProduct underlyingProduct;
        private readonly int productCount;
        private readonly Action<IProduct, int> transaction;
        private IView relatedView;

        private bool transactionPerformed = false;
        private void EnsureTransactionPerformed()
        {
            if (transactionPerformed)
                return;

            PerformTransaction();
        }

        public void PerformTransaction()
        {
            transaction(underlyingProduct, productCount);
            relatedView = new ShoppingCartPage(underlyingUser, new BookLinkGenerator());

            transactionPerformed = true;
        }

        public void GenerateRelatedView(TextWriter outputTarget)
        {
            EnsureTransactionPerformed();
            relatedView.GenerateHtml(outputTarget);
        }
    }
}
