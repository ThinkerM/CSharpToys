using System.Linq;
using System.Text.RegularExpressions;
using NezarkaCommonLibrary.Extensions;
using NezarkaModel;

namespace NezarkaController.CommandsLayer
{
    internal class CommandParser : IParser<ICommand>
    {
        private IStore Store { get; }

        public CommandParser(IStore store)
        {
            Store = store;
        }

        /// <summary>
        /// Constructs an appropriate command from a link.
        /// Returns an InvalidCommand if there is any problem with the link and doesn't provide any sort of feedback :P
        /// (can be invalid format, invalid data types on key positions, etc...)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ICommand Parse(string input)
        {
            input = input.Trim(); //remove any trailing characters

            var linkPattern = new Regex($@"^GET (?<CustomerId>\d+) http://www\.nezarka\.net/(?<BookOrCart>\w+)(?:/(?<MiddleToken>\w+)/(?<ProductId>\d+))?$");

            Match inputMatch = linkPattern.Match(input);
            GroupCollection captureGroups = inputMatch.Groups;

            if (!inputMatch.Success
                || !Store.HasCustomer(int.Parse(captureGroups["CustomerId"].Value)))
                return new InvalidCommand();

            return NavigateAddressTree(captureGroups);
        }

        private ICommand NavigateAddressTree(GroupCollection captureGroups)
        {
            int userId = int.Parse(captureGroups["CustomerId"].Value);
            int nonEmptyAdressGroupsCount = new[]
            {
                !captureGroups["BookOrCart"].Value.IsNullOrEmpty(),
                !captureGroups["MiddleToken"].Value.IsNullOrEmpty(),
                !captureGroups["ProductId"].Value.IsNullOrEmpty()
            }.Count(x => x);

            switch (captureGroups["BookOrCart"].Value)
            {
                case "Books":
                {
                    if (nonEmptyAdressGroupsCount == 1) //no other segments
                            return new ViewBooksMenuCommand(userId, Store);

                    int productId = int.Parse(captureGroups["ProductId"].Value);

                    if (captureGroups["MiddleToken"].Value == "Detail" && Store.HasProduct(productId))
                        return new ViewBookDetailCommand(userId, productId, Store);

                    break;
                }
                case "ShoppingCart":
                {
                    if (nonEmptyAdressGroupsCount == 1) //no other segments
                        return new ViewUserCartCommand(userId, Store);

                    int productId = int.Parse(captureGroups["ProductId"].Value);
                    TransactionCommand.CartAction action;
                    if (Store.HasProduct(productId) && captureGroups["MiddleToken"].Value.TryParseEnum(out action))
                    {
                        if (action == TransactionCommand.CartAction.Remove
                            && !Store.Customer(userId).ShoppingCart.ContainsProduct(productId))
                            break;

                        return new TransactionCommand(Store, userId, productId, 1, action);
                    }
                    break;
                }
            }

            return new InvalidCommand();
        }
    }
}
