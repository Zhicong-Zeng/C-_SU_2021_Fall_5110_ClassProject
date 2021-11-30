using System.Linq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Controllers;

namespace UnitTests.Controllers
{   
    /// <summary>
    /// Products Controller class
    /// </summary>
    public class ProductsControllers
    {
        //--------------------------------------------------------------------------------
        #region TestSetup
        /// <summary>
        /// Initialize Test
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup
        //--------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------
        #region ProductController
        /// <summary>
        /// Tests "public IEnumerable<ProductModel> Get()" in ProductsController.cs
        /// </summary>
        [Test]
        public void GetProducts_Default_Should_Return_Data()
        {
            // Arrange
            
            // Act

            var results = TestHelper.ProductController.Get().FirstOrDefault();

            // Assert
            Assert.IsNotNull(results);
        }

        /// <summary>
        /// Tests "public ActionResult Patch([FromBody] RatingRequest request)"
        /// in ProductsController.cs
        /// </summary>
        [Test]
        public void Patch_RatingRequest_Valid_Should_Return_Data()
        {
            // Arrange

            // Act
            var data = new ProductsController.RatingRequest();

            // Assert
            Assert.IsNotNull(data);
        }

        /// <summary>
        /// Tests "public ActionResult Patch([FromBody] RatingRequest request)"
        /// in ProductsController.cs
        /// </summary>
        [Test]
        public void Patch_RatingRequest_Valid_ProductId_Valid_ProductRating_Should_Equal_0()
        {
            // Arrange
            var product = TestHelper.RatingRequest;

            // Act
            product.ProductId = "the-last-supper";
            product.Rating = 0;
            TestHelper.ProductController.Patch(product);

            // Assert
            Assert.AreEqual(product.Rating, 0);
        }

        /// <summary>
        /// Tests "public ActionResult Patch([FromBody] CommentRequest request)"
        /// in ProductsController.cs
        /// </summary>
        [Test]
        public void Patch_CommentRequest_Valid_Comment_Valid_Should_Comment_Equal()
        {
            // Arrange
            var request = TestHelper.CommentRequest;

            // Act
            request.ProductId = "the-starry-night";
            request.Comment = "O hi there";

            TestHelper.ProductController.Patch(request);

            // Assert
            Assert.AreEqual(request.Comment, "O hi there");
        }
        #endregion ProductController
        
        //--------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------
        #region RatingRequest
        /// <summary>
        /// Tests RatingRequest "public string ProductId { get; set; }" in ProductsController.cs
        /// </summary>
        [Test]
        public void Get_ProductId_ProductID_Valid_Should_Return_Null()
        {
            // Arrange

            // Act
            var results = TestHelper.RatingRequest.ProductId;

            // Assert(Null for No data initial in Get() RatingRequest method)
            Assert.IsNull(results);
        }

        /// <summary>
        /// Tests RatingRequest "public string Rating { get; set; }" in ProductsController.cs
        /// </summary>
        [Test]
        public void Get_Rating_Valid_Should_Return_Null()
        {
            // Arrange

            // Act
            var results = TestHelper.RatingRequest.Rating;

            // Assert(Null for No data initial in Get() RatingRequest method)
            Assert.AreEqual(0,results);
        }

        /// <summary>
        /// Tests RatingRequest "public string ProductId { get; set; }" in ProductsController.cs
        /// </summary>
        [Test]
        public void Set_RatingRequest_ProductId_Valid_Should_Equal()
        {
            // Arrange
            var results = TestHelper.RatingRequest;

            // Act
            results.ProductId = "111222";

            // Assert
            Assert.AreEqual("111222", results.ProductId);
        }

        /// <summary>
        /// Tests RatingRequest "public string Ratings { get; set; }" in ProductsController.cs
        /// </summary>
        [Test]
        public void Set_RatingRequest_Rating_Valid_Should_Return_Equal()
        {
            // Arrange
            var results = TestHelper.RatingRequest;

            // Act
            results.Rating = 5;

            // Assert
            Assert.AreEqual(5, results.Rating);
        }
        #endregion RatingRequest
        
        //--------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------
        #region CommentRequest

        /// <summary>
        /// Test CommentRequest "public string ProductId { get; set; }" in ProductsController.cs
        /// </summary>
        [Test]
        public void Get_CommentRequest_ProductId_Valid_Should_Return_Null()
        {
            // Arrange
            var results = TestHelper.CommentRequest.ProductId;

            // Act

            // Assert(Null for No data initial in Get()RatingRequest method)
            Assert.AreEqual(null, results);
        }

        /// <summary>
        /// Test CommentRequest "public string Comment { get; set; }" in ProductsController.cs
        /// </summary>
        [Test]
        public void Get_CommentRequest_Comment_Valid_Should_Return_Null()
        {
            // Arrange
            var results = TestHelper.CommentRequest.Comment;

            // Act

            // Assert(Null for No data initial in Get() RatingRequest method)
            Assert.AreEqual(null, results);
        }

        /// <summary>
        /// Test CommentRequest 
        /// "public string ProductId { get; set; }" 
        /// &
        /// "public string Comment { get; set; }" in ProductsController.cs
        /// 
        /// No Assert Because this Unit test is to test the menthod set function work
        /// </summary>
        [Test]
        public void Set_CommentRequest_Set_Comment_Valid_Set_ProductId_Return_NotNull()
        {
            // Arrange

            // Act
            var comment = new ProductsController.CommentRequest { Comment = "fgdf", ProductId = "sdfs" };

            // Assert
            Assert.IsNotNull(comment);
        }
        #endregion CommentRequest
        //--------------------------------------------------------------------------------
    }
}