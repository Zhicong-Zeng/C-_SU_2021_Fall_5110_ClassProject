using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Home
{
    /// <summary>
    /// Index test unit test
    /// </summary>
    public class IndexTests
    {
        #region TestSetup
        // Page model declaration
        public static HomeModel pageModel;

        /// <summary>
        /// Initialize mocks and models
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            var MockLoggerDirect = Mock.Of<ILogger<HomeModel>>();
            pageModel = new HomeModel(MockLoggerDirect, TestHelper.ProductService);
        }
        #endregion TestSetup

        /// <summary>
        /// Check Products get method 
        /// </summary>
        #region ProductGet
        [Test]
        public void PageModel_Get_Products_Valid_Assert_Should_Return_Data()
        {
            // Arrange

            // Act
            pageModel.OnGet();
            var data = pageModel.Products;

            // Assert
            Assert.IsNotNull(data);
        }
        #endregion ProductGet
    }
}