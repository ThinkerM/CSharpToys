using NezarkaView.Links;
using NUnit.Framework;

namespace NezarkaTests.View
{
    [TestFixture]
    public class LinkHelperTests
    {
        [Test]
        [TestCase("Book", "Book")]
        [TestCase("Book/Details", "Book", "Details")]
        [TestCase("Book/Details/450", "Book", "Details", "450")]
        [TestCase("GET 5 http://www.nezarka.net/ShoppingCart/Add/450",
            "GET 5 http://www.nezarka.net", "ShoppingCart", "Add", "450")]
        [TestCase("", "")]
        [TestCase("/ShoppingCart", "/ShoppingCart", "")]
        [TestCase("/ShoppingCart", "", "/ShoppingCart", "")]
        public void ConstructPathTest(string expectedResult, params string[] segments)
        {
            Assert.AreEqual(expectedResult, LinkingHelper.ConstructPath(segments));
        }
    }
}