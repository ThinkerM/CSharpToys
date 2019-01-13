using System.IO;
using NezarkaModel;
using NezarkaView;
using NezarkaView.Links;
using NezarkaView.Pages;

namespace NezarkaController.CommandsLayer
{
    internal class ViewBookDetailCommand : ICommand
    {
        public ViewBookDetailCommand(int userId, int productId, IStore store)
        {
            ICustomer user = store.Customer(userId);
            var product = store.Product<ICopyrightable>(productId);
            underlyingPage = new ProductDetailsPage(user, new BookLinkGenerator(), product, "book");
        }

        private readonly IView underlyingPage;
        public void GenerateRelatedView(TextWriter outputTarget)
        {
            underlyingPage.GenerateHtml(outputTarget);
        }
    }
}
