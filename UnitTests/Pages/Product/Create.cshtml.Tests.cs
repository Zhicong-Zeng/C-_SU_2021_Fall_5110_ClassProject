using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;

namespace UnitTests.Pages.Product.Create
{
    /// <summary>
    /// Create test for class for the product/create
    /// </summary>
    public class CreateTests
    {
        // Test method for the pageModel
        #region TestSetup
        public static CreateModel pageModel;

        /// <summary>
        /// method to initialite the test
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new CreateModel(TestHelper.ProductService);
        }
        #endregion TestSetup

        /// <summary>
        /// method to check that the onGet method returns Products from the Products.json
        /// </summary>
        #region OnGet
        [Test]
        public void ProductService_OnGet_Valid_Assert_ModelState_Should_True()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }
        #endregion OnGet

        /// <summary>
        /// method to check that the GetProducts() method returns data from the Products.json
        /// </summary>
        #region GetProducts
        [Test]
        public void ProductService_GetProducts_Valid_Assert_Should_Equal_Data()
        {
            // Arrange

            // Act
            var Data = TestHelper.ProductService.GetProducts().Count();

            // Assert
            Assert.AreEqual(Data, TestHelper.ProductService.GetProducts().Count());
        }
        #endregion GetProducts
    }
}