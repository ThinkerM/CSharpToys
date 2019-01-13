using NezarkaController;
using NezarkaController.CommandsLayer;
using NezarkaModel;
using NezarkaModel.Entities;
using NUnit.Framework;

namespace NezarkaTests.Controller
{
    [TestFixture]
    public class CommandParserTests
    {

        private IStore testStore;
        private IParser<ICommand> parser;

        [SetUp]
        public void Initialize()
        {
            testStore = new TestStore();
            parser = new CommandParser(testStore);
        }

        [Test]
        [TestCase("GET 4 http://www.nezarka.net/Books")]
        public void ParseBooksMenuLinkTest(string link)
        {
            var parsedCommand = parser.Parse(link);
            Assert.IsNotNull(parsedCommand);
            Assert.IsInstanceOf<ViewBooksMenuCommand>(parsedCommand);
        }

        [Test]
        [TestCase("GET 4 http://www.nezarka.net/Books/Detail/5")]
        public void ParseBookDetailLinkTest(string link)
        {
            var parsedCommand = parser.Parse(link);
            Assert.IsNotNull(parsedCommand);
            Assert.IsInstanceOf<ViewBookDetailCommand>(parsedCommand);
        }

        [Test]
        [TestCase("GET 4 http://www.nezarka.net/ShoppingCart")]
        public void ParseShoppingCartLinkTest(string link)
        {
            var parsedCommand = parser.Parse(link);
            Assert.IsNotNull(parsedCommand);
            Assert.IsInstanceOf<ViewUserCartCommand>(parsedCommand);
        }

        [Test]
        [TestCase("GET 4 http://www.nezarka.net/ShoppingCart/Add/5")]
        [TestCase("GET 1 http://www.nezarka.net/ShoppingCart/Remove/1")]
        public void ParseTransactionCommandLinkTest(string link)
        {
            testStore.Customer(1).ShoppingCart.Add(new Book(1,"1","1",1));

            var parsedCommand = parser.Parse(link);
            Assert.IsNotNull(parsedCommand);
            Assert.IsInstanceOf<ITransactionCommand>(parsedCommand);
        }

        [Test]
        [TestCase("GET 0 http://www.nezarka.net/Books")] //Id must be nonnegative
        [TestCase("GET -1 http://www.nezarka.net/Books/Detail/5")] //Negative Customer Id
        [TestCase("GET 1 http://www.na.net/Books")] //Faulty link
        [TestCase("SET 1 http://www.nezarka.net/Books")] //Faulty initial command
        [TestCase("GET 1 http://www.nezarka.netBooks")] //Faulty link
        [TestCase("GET 1 Books")] //Faulty link
        [TestCase("GET 2339 http://www.nezarka.net/Books")] //Nonexistent customer
        [TestCase("GET 3 http://www.nezarka.net/ShoppingCart/Remove/-234")] //Negative product Id
        [TestCase("GET 4 http://www.nezarka.net/Books/")]
        [TestCase("GET 4 http://www.nezarka.net/ShoppingCart/Add /4")]
        [TestCase("GET 4 www.nezarka.net/Books")]
        [TestCase("GET 4 http://www.nezarka.net//Books")]
        [TestCase("GET 4 http://www.nezarka.net/Books/Remove/3")]
        [TestCase("GET 4 http://www.nezarka.netBooks")]
        public void ParseFaultyLinkTest(string link)
        {
            var parsedCommand = parser.Parse(link);
            Assert.IsInstanceOf<InvalidCommand>(parsedCommand);
        }
    }
}