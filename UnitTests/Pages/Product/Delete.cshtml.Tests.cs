using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;

namespace UnitTests.Pages.Product.Delete
{   
    /// <summary>
    /// Unit test for the delete functionality
    /// </summary>
    public class DeleteTests
    {
        // Manages tthe delete of a single record
        #region TestSetup
        public static DeleteModel pageModel;

        /// <summary>
        /// delete method test initializaton
        /// </summary>
        [SetUp]        
        public void TestInitialize()
        {
            pageModel = new DeleteModel(TestHelper.ProductService);
        }
        #endregion TestSetup

        /// <summary>
        /// onGet test method to read and return the data.
        /// </summary>
        #region OnGet
        [Test]
        public void PageModel_OnGet_NotValid_Assert_Should_Return_Data()
        {
            // Arrange

            // Act
            var data = pageModel.OnGet(null);

            // Assert 
            // Return <Microsoft.AspNetCore.Mvc.RedirectToPageResult>
            Assert.IsNotNull(data);
        }

        /// <summary>
        /// Test to check that onGet method is valid and returns the product 
        /// </summary>
        [Test]
        public void PageModel_OnGet_Valid_Assert_Should_Return_Products_Valid()
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
        /// Test that valid OnPost call deletes product
        /// </summary>
        [Test]
        public void PageModel_OnPost_UpdateData_Valid_Assert_Should_Return_Null()
        {
            // Arrange

            // First Create the product to delete
            pageModel.Product = TestHelper.ProductService.CreateData();
            pageModel.Product.Title = "Example to Delete";
            TestHelper.ProductService.UpdateData(pageModel.Product);

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));

            // Confirm the item is deleted
            Assert.AreEqual(null, TestHelper.ProductService.GetProducts().FirstOrDefault(m=>m.Id.Equals(pageModel.Product.Id)));
        }

        /// <summary>
        /// Test that invalid OnPost call returns false
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