using NUnit.Framework;
using System.Linq;

namespace UnitTests.Services.JsonFileProductService.GetAverageRating
{
    /// <summary>
    /// Json Product services test
    /// </summary>
    public class JsonFileProductServiceGetAverageRating
    {
        #region TestSetup
        /// <summary>
        /// Initializes Test
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion TestSetup

        #region GetAverageRating
        /// <summary>
        /// Tests GetAverageRating to return average rating
        /// uses "american-gothic" product with known average rating of 2
        /// </summary>
        [Test]
        public void ProductService_GetAverageRating_Valid_Assert_Should_Equal()
        {
            // Arrange
            var data = TestHelper.ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals("american-gothic"));

            // Act
            var result = TestHelper.ProductService.GetAverageRating(data);

            // Assert
            Assert.AreEqual(2, result);
        }

        /// <summary>
        /// Tests GetAverageRating to return average rating if rating is null
        /// uses "619737e3-5880-4c1e-95d6-c079346568aa" with known rating of null
        /// </summary>
        [Test]
        public void ProductService_GetAverageRating_Valid_Rating_Null_Assert_Should_Equal()
        {
            // Arrange
            //619737e3-5880-4c1e-95d6-c079346568aa productID has null ratings
            var data = TestHelper.ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals("619737e3-5880-4c1e-95d6-c079346568aa"));

            // Act
            var result = TestHelper.ProductService.GetAverageRating(data);

            // Assert
            Assert.AreEqual(0, result);
        }
        #endregion GetAverageRating
    }
}