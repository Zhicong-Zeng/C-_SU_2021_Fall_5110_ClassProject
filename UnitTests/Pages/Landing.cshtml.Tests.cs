using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Landing
{
    /// <summary>
    /// Tests Landing.cshtml.cs functionality
    /// </summary>
    public class LandingTests
    {
        #region TestSetup
        public static LandingModel pageModel;

        /// <summary>
        /// initializes Tests annd pagemodel
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new LandingModel(TestHelper.ProductService);
        }
        #endregion TestSetup

        #region GetProductService
        /// <summary>
        /// Tests "public JsonFileProductService ProductService"
        /// </summary>
        [Test]
        public void ProductService_Get_Valid_Assert_Should_Return_Data()
        {
            // Arrange
            pageModel.OnGet();

            // Act
            var data = pageModel.ProductService;

            // Assert 
            Assert.IsNotNull(data);
        }
        #endregion GetProductService

        #region GetProducts
        /// <summary>
        /// Tests "public IEnumerable<ProductModel> Products"
        /// </summary>
        [Test]
        public void PageModel_Products_OnGet_Valid_Assert_Should_Return_Null()
        {
            // Arrange
            pageModel.OnGet();

            // Act
            var data = pageModel.Products;

            // Assert()(Null for No data in Get() Products method)
            Assert.IsNull(data);
        }
        #endregion GetProducts

        #region GetTopThreeArtworks
        /// <summary>
        /// Tests "public List<ProductModel> TopThreeArtwork"
        /// </summary>
        [Test]
        public void TopThreeArtwork_Get_Valid_Assert_Should_Return_Data()
        {
            // Arrange

            // Act
            pageModel.OnGet();
            var data = pageModel.TopThreeArtwork;

            // Assert
            Assert.IsNotNull(data);
        }

        /// <summary>
        /// Tests "public List<ProductModel> TopThreeArtwork"
        /// </summary>
        [Test]
        public void TopThreeArtwork_Set_Valid_Assert_Should_Return_Data()
        {
            // Arrange

            // Act
            pageModel.OnGet();
            var data = pageModel.TopThreeArtwork;

            // Assert
            Assert.IsNotNull(data);
        }
        #endregion GetTopThreeArtworks
    }
}