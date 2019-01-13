using System.IO;
using System.Text;
using NezarkaModel.Entities;
using NezarkaTests.Properties;
using NezarkaView.Components;
using NezarkaView.Components.Cart;
using NezarkaView.Components.Products;
using NezarkaView.Links;
using NUnit.Framework;

namespace NezarkaTests.View
{
    [TestFixture]
    public class ComponentsTests
    {
        private StringBuilder resultHtml;
        private StringWriter htmlWriter;
        private readonly ILinkGenerator linkGen = new TestLinkGenerator();

        [SetUp]
        public void InitializeWriting()
        {
            resultHtml = new StringBuilder();
            htmlWriter = new StringWriter(resultHtml);
        }

        [Test]
        public void CartItemViewSingleCountHtmlTest()
        {
            var itemView =
                new CartItemView(new ShoppingCartItem(new Book(3, "TestBook", "FakeAuthor", price: 10)), linkGen);
            itemView.GenerateHtml(htmlWriter, 2);

            Assert.AreEqual(Resources.ExpectedCartItemSingleView, resultHtml.ToString());
        }

        [Test]
        public void CartItemViewMultipleCountHtmlTest()
        {
            var itemView =
                new CartItemView(new ShoppingCartItem(new Book(3, "TestBook", "FakeAuthor", price: 10), count: 3), linkGen);
            itemView.GenerateHtml(htmlWriter, 2);

            Assert.AreEqual(Resources.ExpectedCartItemMultipleView, resultHtml.ToString());
        }

        [Test]
        public void CartTableViewHtmlTest()
        {
            var item1 = new ShoppingCartItem(product: new Book(1, "TestBook", "FakeAuthor", price: 10), count: 1);
            var item2 = new ShoppingCartItem(product: new Book(2, "TestBook2", "FakeAuthor", price: 10), count: 3);

            var items = new[] { item2, item1 };

            new CartTableView(items, linkGen).GenerateHtml(htmlWriter, 1);
            Assert.AreEqual(Resources.ExpectedCartTableView, resultHtml.ToString());
        }

        [Test]
        public void CopyrightableProductInfoViewHtmlTest()
        {
            new CopyrightableProductInfoView(new Book(3, "Going Postal", "Terry Pratchett", 28), linkGen, typeof(Book).Name)
                .GenerateHtml(htmlWriter, 1);

            Assert.AreEqual(Resources.ExpectedBookDetails, resultHtml.ToString());
        }

        [Test]
        public void CopyrightableProductListItemViewHtmlTest()
        {
            new CopyrightableProductListItemView(new Book(1, "Lord of the Rings", "J. R. R. Tolkien", 59), linkGen)
                .GenerateHtml(htmlWriter, 3);

            Assert.AreEqual(Resources.ExpectedListItemView, resultHtml.ToString());
        }

        [Test]
        public void CopyrightableProductListViewHtmlTest()
        {
            var testProduct1 = new Book(1, "Lord of the Rings", "J. R. R. Tolkien", 59);
            var testProduct2 = new Book(2, "Hobbit: There and Back Again", "J. R. R. Tolkien", 49);
            var testProduct3 = new Book(3, "Going Postal", "Terry Pratchett", 28);
            var testProduct4 = new Book(4, "The Colour of Magic", "Terry Pratchett", 159);
            var testProduct5 = new Book(5, "I Shall Wear Midnight", "Terry Pratchett", 31);

            var allProducts = new[] { testProduct1, testProduct2, testProduct3, testProduct4, testProduct5 };
            new CopyrightableProductListView(allProducts, linkGen, 3)
                .GenerateHtml(htmlWriter, 1);

            Assert.AreEqual(Resources.ExpectedBookListView, resultHtml.ToString());
        }

        [Test]
        public void CustomerCommonHeaderViewHtmlTest()
        {
            var testCustomer = new Customer(100, "Jan", "Novak");
            new CustomerMenuView(testCustomer, linkGen)
                .GenerateHtml(htmlWriter, 1);

            Assert.AreEqual(Resources.ExpectedCustomerHeader, resultHtml.ToString());
        }

        [Test]
        public void PageStyleSetupViewHtmlTest()
        {
            new NezarkaPageStyleSetup().GenerateHtml(htmlWriter, tabsOffset: 1);

            Assert.AreEqual(Resources.ExpectedStyleSetup, resultHtml.ToString());
        }
    }
}