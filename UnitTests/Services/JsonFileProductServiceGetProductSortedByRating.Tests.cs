using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.Services.JsonFileProductService.GetProductSortedByRating
{
    /// <summary>
    /// Json Product services test for sorting by artist name
    /// </summary>
    public class JsonFileProductServiceGetProductSortedByRating
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

        #region  GetProductSortedByDescRating
        /// <summary>
        /// Test that method returns a sorted list when called
        /// </summary>
        [Test]
        public void GetProductSortedByDescRating_Valid_Should_Return_Sorted_Products()
        {
            // Arrange

            // Act
            var productsSorted = TestHelper.ProductService.GetProductSortedByDescRating();

            // Assert
            for (int i = 1; i < productsSorted.Count(); i++)
            {
                if (productsSorted.ElementAt(i).Ratings == null || productsSorted.ElementAt(i).Ratings == null)
                {
                    break;
                }
                double previous = productsSorted.ElementAt(i - 1).Ratings.Average();
                double current = productsSorted.ElementAt(i).Ratings.Average();

                // check that the previous item is not the same as the current
                Assert.IsTrue((((int)previous) - ((int)current)) >= 0);
            }
        }
        #endregion  GetProductSortedByDescRating

        #region  GetProductSortedByAscRating
        /// <summary>
        /// Test that method returns a sorted list when called
        /// </summary>
        [Test]
        public void GetProductSortedByAscRating_Valid_Should_Return_Sorted_Products()
        {
            // Arrange
            var products = TestHelper.ProductService.GetProducts();
            var productsSorted = TestHelper.ProductService.GetProductSortedByAscRating();
            var listOfRatings = new List<int>();

            // Act
            //adds rating for each product into list
            foreach (var item in products)
            {
                listOfRatings.Add(TestHelper.ProductService.GetAverageRating(item));
            }
            //order list by ascending order
            listOfRatings = listOfRatings.OrderBy(x => x).ToList();

            // Assert
            for (int i = 0; i < productsSorted.Count(); i++)
            {
                if (productsSorted.ElementAt(i).Ratings != null)
                {
                    Assert.AreEqual((int)productsSorted.ElementAt(i).Ratings.Average(), listOfRatings[i]);
                }
            }
        }
        #endregion  GetProductSortedByAscRating
    }
}