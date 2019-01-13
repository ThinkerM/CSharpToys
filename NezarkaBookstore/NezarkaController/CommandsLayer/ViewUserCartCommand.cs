using System.IO;
using NezarkaModel;
using NezarkaView;
using NezarkaView.Links;
using NezarkaView.Pages;

namespace NezarkaController.CommandsLayer
{
    internal class ViewUserCartCommand : ICommand
    {
        public ViewUserCartCommand(int userId, IStore store)
        {
            ICustomer user = store.Customer(userId);
            underlyingPage = new ShoppingCartPage(user, new BookLinkGenerator());
        }

        private readonly IView underlyingPage;
        public void GenerateRelatedView(TextWriter outputTarget)
        {
            underlyingPage.GenerateHtml(outputTarget);
        }
    }
}
