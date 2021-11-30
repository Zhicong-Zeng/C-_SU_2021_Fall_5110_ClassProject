using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;

namespace UnitTests.Pages.Product.Index
{
    /// <summary>
    /// Unit test for the index functionality
    /// </summary>
    public class IndexTests
    {
        // Test to see the management of index file
        #region TestSetup
        public static PageContext pageContext;
        public static IndexModel pageModel;

        /// <summary>
        /// Test Initialize method
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new IndexModel(TestHelper.ProductService);
        }
        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// onGet test to return a list of products not sorted
        /// </summary>
        [Test]
        public void PageMode_OnGet_Valid_Assert_Should_Return_Data()
        {
            // Arrange
            pageModel.OnGet(null);

            // Act
            var data = pageModel.Products.FirstOrDefault();

            // Assert
            Assert.IsNotNull(data);
        }

        /// <summary>
        /// onGet test to return a list of products sorted by artist
        /// </summary>
        [Test]
        public void PageMode_OnGet_Valid_Sort_By_Artist_Assert_Should_Return_Sorted_Data()
        {
            // Arrange
            pageModel.OnGet("artist_asc");
            var products = TestHelper.ProductService.GetProducts();

            // Act
            products = products.OrderBy(x => x.Artist);
            var pageProducts = pageModel.Products;

            // Assert
            for (int i = 0; i < products.Count(); i++)
            {
                Assert.AreEqual(products.ElementAt(i).Artist, pageProducts.ElementAt(i).Artist);
            }
        }

        /// <summary>
        /// onGet test to return a list of products sorted by title
        /// </summary>
        [Test]
        public void PageMode_OnGet_Valid_Sort_By_Title_Assert_Should_Return_Sorted_Data()
        {
            // Arrange
            pageModel.OnGet("title_asc");
            var products = TestHelper.ProductService.GetProducts();

            // Act
            products = products.OrderBy(x => x.Title);
            var pageProducts = pageModel.Products;

            // Assert
            for (int i = 0; i < products.Count(); i++)
            {
                Assert.AreEqual(products.ElementAt(i).Title, pageProducts.ElementAt(i).Title);
            }
        }

        /// <summary>
        /// onGet test to return a list of products sorted by title
        /// </summary>
        [Test]
        public void PageMode_OnGet_Valid_Sort_By_Rating_Desc_Assert_Should_Return_Sorted_Data()
        {
            // Arrange
            pageModel.OnGet("rating_desc");

            // Act
            var data = pageModel.Products.FirstOrDefault();

            // Assert
            Assert.IsNotNull(data);          
        }

        /// <summary>
        /// onGet test to return a list of products sorted by rating
        /// </summary>
        [Test]
        public void PageMode_OnGet_Valid_Sort_By_Rating_Asc_Assert_Should_Return_Sorted_Data()
        {
            // Arrange
            pageModel.OnGet("rating_asc");
            var products = TestHelper.ProductService.GetProducts();

            // Act
            var data = pageModel.Products.FirstOrDefault();

            // Assert
            Assert.IsNotNull(data);
        }

        /// <summary>
        /// onGet test to return a list of products sorted by Desc.Title()
        /// </summary>
        [Test]
        public void PageMode_OnGet_Valid_Sort_By_Title_Desc_Assert_Should_Return_Sorted_Data()
        {
            // Arrange
            pageModel.OnGet("title_desc");
            var products = TestHelper.ProductService.GetProducts();

            // Act
            var data = pageModel.Products.FirstOrDefault();

            // Assert
            Assert.IsNotNull(data);
        }

        /// <summary>
        /// onGet test to return a list of products sorted by Desc.Artist()
        /// </summary>
        [Test]
        public void PageMode_OnGet_Valid_Sort_By_Artist_Desc_Assert_Should_Return_Sorted_Data()
        {
            // Arrange
            pageModel.OnGet("artist_desc");

            // Act
            var data = pageModel.Products.FirstOrDefault();

            // Assert
            Assert.IsNotNull(data);
        }
        #endregion OnGet
    }
}