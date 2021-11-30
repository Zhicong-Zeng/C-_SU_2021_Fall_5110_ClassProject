using NUnit.Framework;
using System.Linq;

namespace UnitTests.Services.JsonFileProductService.AddRating
{
    /// <summary>
    /// Json File Product service test
    /// </summary>
    public class JsonFileProductServiceAddRatingTests
    {
        #region TestSetup
        /// <summary>
        /// Initialize setup
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion TestSetup

        /// <summary>
        /// Test funciton for rating null items
        /// </summary>
        #region AddRating
        [Test]
        public void ProductService_AddRating_Valid_ProductId_Null_Rating_Valid_Assert_Should_False()
        {
            // Arrange
            var data = TestHelper.ProductService.AddRating(null, 1);

            // Act

            // Assert
            Assert.AreEqual(false, data);
        }

        /// <summary>
        /// Test function to checking rating of invalid item
        /// </summary>
        [Test]
        public void ProductService_AddRating_Valid_ProducId_InValid_Rating_Valid_Assert_Should_False()
        {
            // Arrange
            var data = TestHelper.ProductService.AddRating("fdg", 5);

            // Act

            // Assert
            Assert.AreEqual(false, data);
        }

        /// <summary>
        /// Test function to test when rating is less than 0 and return false
        /// </summary>
        [Test]
        public void ProductService_AddRating_Valid_Product_Valid_Rating_SmallThanZero_Assert_Should_Return_False()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().First();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, -1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Test function for add rating to return false when rating is bigger than 5 and return false
        /// </summary>
        [Test]
        public void ProductService_AddRating_Valid_Product_Valid_AddRating_BiggerThanFive_Assert_Should_Return_False()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().First();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// function to test the rating when it is null
        /// </summary>
        [Test]
        public void ProductService_AddRating_Valid_Product_Valid_AddRating_Valid_Assert_Return_True()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().Last();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 4);

            // Assert
            Assert.AreEqual(true , result);
        }

        /// <summary>
        /// Function to test the add rating functionality
        /// </summary>
        [Test]
        public void ProductService_AddRating_Valid_Product_Valid_Rating_Valid_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetProducts().First();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 5);

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion AddRating
    }
}