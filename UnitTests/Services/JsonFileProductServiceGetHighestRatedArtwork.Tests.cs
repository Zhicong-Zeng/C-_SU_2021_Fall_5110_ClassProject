using NUnit.Framework;
using System.Linq;

namespace UnitTests.Services.JsonFileProductService.GetHighestRatedArtwork
{
    /// <summary>
    /// Json Product services test
    /// </summary>
    public class JsonFileProductServiceGetHighestRatedArtwork
    {
        #region TestSetup
        /// <summary>
        /// Initializes Tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion TestSetup

        #region GetHighestRatedArtwork
        /// <summary>
        /// Checks if GetHighestRatedArtwork returns list of three entries.
        /// Impossible to verify list's accuracy as ratings will change.
        /// </summary>
        [Test]
        public void ProductService_GetHighestRatedArtwork_Valid_Assert_Should_Valid()
        {
            // Arrange
            var data = TestHelper.ProductService.GetHighestRatedArtwork();

            // Act
            var result = data.Count();

            // Assert
            Assert.AreEqual(3, result);
        }        
        #endregion GetHighestRatedArtork
    }
}