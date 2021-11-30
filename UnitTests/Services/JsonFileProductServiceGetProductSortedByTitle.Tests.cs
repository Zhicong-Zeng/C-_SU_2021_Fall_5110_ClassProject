using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests.Services.JsonFileProductService.GetProductSortedByTitle
{
	/// <summary>
	/// Json Product services test for sorting by title name
	/// </summary>
	public class JsonFileProductServiceGetProductSortedByTitle
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

        #region GetProductSortedByTitle
        /// <summary>
        /// Test that method returns a sorted list when called
        /// </summary>
        [Test]
        public void GetProductSortedByTitle_Valid_Should_Return_Sorted_Products()
        {
            // Arrange

            //// Act
            var productsSorted = TestHelper.ProductService.GetProductSortedByAscTitle();

            // Assert
            for (int i = 1; i < productsSorted.Count(); i++)
            {
                string previous = productsSorted.ElementAt(i - 1).Title;
                string current = productsSorted.ElementAt(i).Title;

                // check that the previous item is less than or equal to the current
                Assert.True(String.Compare(previous, current) <= 0);
            }
        }
        #endregion GetProductSortedByTitle
    }
}