using NezarkaModel;
using NezarkaModel.Entities;
using NezarkaView.Links;
using NUnit.Framework;

namespace NezarkaTests.View
{
    [TestFixture]
    public class BookLinkGeneratorTests
    {
        private IProduct product;
        private BookLinkGenerator generator;

        [SetUp]
        public void Initialization()
        {
            product = new Book(500, "Nice Book", "Mr. Canister", 10);
            generator = new BookLinkGenerator();
        }

        [Test]
        public void ProductsMenuLinkTest()
        {
            string expectedLink = "/Books";
            Assert.AreEqual(expectedLink, generator.ProductsMenuLink());
        }

        [Test]
        public void ProductDetailLinkTest()
        {
            string expectedLink = $"/Books/Detail/{product.Id}";
            Assert.AreEqual(expectedLink, generator.ProductDetailLink(product.Id));
        }

        [Test]
        public void ShoppingCartLinkTest()
        {
            string expectedLink = "/ShoppingCart";
            Assert.AreEqual(expectedLink, generator.ShoppingCartLink());
        }

        [Test]
        public void AddToCartLinkTest()
        {
            string expectedLink = $"/ShoppingCart/Add/{product.Id}";
            Assert.AreEqual(expectedLink, generator.AddToCartLink(product.Id));
        }

        [Test]
        public void RemoveFromCartLinkTest()
        {
            string expectedLink = $"/ShoppingCart/Remove/{product.Id}";
            Assert.AreEqual(expectedLink, generator.RemoveFromCartLink(product.Id));

        }
    }
}
