using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.Update
{   
    /// <summary>
    /// Unit test for the  functionality
    /// </summary>
    public class UpdateTests
    {
        #region TestSetup
        /// <summary>
        /// Test setup method
        /// </summary>
        public static UpdateModel pageModel;

        /// <summary>
        /// Test initializer
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            pageModel = new UpdateModel(TestHelper.ProductService);
        }

        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test OnGet method to retrun product
        /// </summary>
        [Test]
        public void PageModel_OnGet_Valid_Assert_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet("the-starry-night");

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("The Starry Night", pageModel.Product.Title);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// test onPost method to return new product
        /// </summary>
        [Test]
        public void PageModel_OnPost_Valid_Assert_Should_Return_Products()
        {
            // Arrange
            pageModel.Product = new ProductModel
            {
                Id = "selinazawacki-moon",
                Title = "title",
                Description = "description",
                Image = "image"
            };

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }

        /// <summary>
        /// onPost methhod to handle errors
        /// </summary>
        [Test]
        public void PageModel_OnPost_InValid_Model_NotValid_Assert_Return_False()
        {
            // Arrange

            // Force an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            var result = pageModel.OnPost() as ActionResult;

            // Assert
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
            Assert.AreEqual(result, result);
        }
        #endregion OnPost
    }
}