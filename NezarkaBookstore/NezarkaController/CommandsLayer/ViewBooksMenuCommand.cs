using System.Collections.Generic;
using System.IO;
using NezarkaModel;
using NezarkaView;
using NezarkaView.Links;
using NezarkaView.Pages;

namespace NezarkaController.CommandsLayer
{
    internal class ViewBooksMenuCommand : ICommand
    {
        public ViewBooksMenuCommand(int userId, IStore store)
        {
            ICustomer user = store.Customer(userId);
            IEnumerable<ICopyrightable> availableProducts = store.Products<ICopyrightable>();
            underlyingPage = new ItemsOfferPage(user, new BookLinkGenerator(), availableProducts, 3, "books");
        }

        private readonly IView underlyingPage;
        public void GenerateRelatedView(TextWriter outputTarget)
        {
            underlyingPage.GenerateHtml(outputTarget);
        }
    }
}
