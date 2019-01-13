using System.IO;
using System.Text;
using NezarkaModel;
using NezarkaModel.Entities;
using NezarkaTests.Properties;
using NezarkaView.Links;
using NezarkaView.Pages;
using NUnit.Framework;

namespace NezarkaTests.View
{
    [TestFixture]
    public class PagesTests
    {
        private StringBuilder resultHtml;
        private StringWriter htmlWriter;
        private StreamWriter fileWriter;
        private ICustomer testUser;
        private readonly ILinkGenerator linkGen = new TestLinkGenerator();
        private static readonly string GeneratedTestFilesFolderPath = $@"";
        private static bool generateFiles = false;

        [SetUp]
        public void InitializeWriting()
        {
            resultHtml = new StringBuilder();
            htmlWriter = new StringWriter(resultHtml);
        }

        [SetUp]
        public void InitializeTestCustomer()
        {
            testUser = new Customer(1, "Jan", "Novak");
            testUser.ShoppingCart.Add(new Book(1, "Lord of the Rings", "J. R. R. Tolkien", 59), count: 3);
            testUser.ShoppingCart.Add(new Book(5, "I Shall Wear Midnight", "Terry Pratchett", 31));
            testUser.ShoppingCart.Add(new Book(3, "Going Postal", "Terry Pratchett", 28));
        }

        [TearDown]
        public void ReleaseResources()
        {
            htmlWriter?.Dispose();
            fileWriter?.Dispose();
        }

        private TextWriter SetFileWriterDestination(string fileName)
        {
            var targetFile = new FileInfo(Path.Combine(PagesTests.GeneratedTestFilesFolderPath, fileName));
            fileWriter = targetFile.CreateText();
            return fileWriter;
        }

        [Test]
        public void InvalidRequestPageTest()
        {
            var invalidRequestPage = new InvalidRequestPage();
            invalidRequestPage.GenerateHtml(htmlWriter);
            if(PagesTests.generateFiles)
                invalidRequestPage.GenerateHtml(SetFileWriterDestination("Invalid Request.html"));

            Assert.AreEqual(Resources.InvalidRequestPageTemplate, resultHtml.ToString());
        }

        [Test]
        public void ItemsOfferPageTest()
        {
            ICopyrightable testProduct1 = new Book(1, "Lord of the Rings", "J. R. R. Tolkien", 59);
            ICopyrightable testProduct2 = new Book(2, "Hobbit: There and Back Again", "J. R. R. Tolkien", 49);
            ICopyrightable testProduct3 = new Book(3, "Going Postal", "Terry Pratchett", 28);
            ICopyrightable testProduct4 = new Book(4, "The Colour of Magic", "Terry Pratchett", 159);
            ICopyrightable testProduct5 = new Book(5, "I Shall Wear Midnight", "Terry Pratchett", 31);
            var products = new[] { testProduct1, testProduct2, testProduct3, testProduct4, testProduct5 };

            var offersPage = new ItemsOfferPage(testUser, linkGen, products, 3, testProduct1.GetType().Name + "s");
            offersPage.GenerateHtml(htmlWriter);
            if (PagesTests.generateFiles)
                offersPage.GenerateHtml(SetFileWriterDestination("Offers Page.html"));

            Assert.AreEqual(Resources.BooksMenuPageTemplate, resultHtml.ToString());
        }

        [Test]
        public void ProductDetailsPageTest()
        {
            ICopyrightable testProduct = new Book(3, "Going Postal", "Terry Pratchett", 28);

            var productPage = new ProductDetailsPage(testUser, linkGen, testProduct, testProduct.GetType().Name);
            productPage.GenerateHtml(htmlWriter);
            if (PagesTests.generateFiles)
                productPage.GenerateHtml(SetFileWriterDestination("Product Details Page.html"));

            Assert.AreEqual(Resources.BookDetailsPageTemplate, resultHtml.ToString());
        }

        [Test]
        public void ShoppingCartNonEmptyTest()
        {
            var cartPage = new ShoppingCartPage(testUser, linkGen);
            cartPage.GenerateHtml(htmlWriter);
            if (PagesTests.generateFiles)
                cartPage.GenerateHtml(SetFileWriterDestination("Shopping cart page.html"));

            Assert.AreEqual(Resources.ShoppingCartPageTemplate, resultHtml.ToString());
        }

        [Test]
        public void ShoppingCartEmptyTest()
        {
            testUser = new Customer(1, "Pavel", "Smith");
            testUser.ShoppingCart.Items.Clear();

            var emptyCartPage = new ShoppingCartPage(testUser, linkGen);
            emptyCartPage.GenerateHtml(htmlWriter);
            if (PagesTests.generateFiles)
                emptyCartPage.GenerateHtml(SetFileWriterDestination("Empty cart page.html"));

            Assert.AreEqual(Resources.EmptyShoppingCartPageTemplate, resultHtml.ToString());
        }
    }
}