using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    [ApiController]

    [Route("[controller]")]
    
    /// <summmary>
    /// Controller class for the products.json
    /// </summmary>   
    public class ProductsController : ControllerBase
    {
    
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Product service get function
        /// </summary>
        public JsonFileProductService ProductService { get; }

        [HttpGet]
        /// <summary>
        ///Product service http get function
        /// </summary>
        /// <returns>Enumerable collection of products</returns>
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetProducts();
        }

        [HttpPatch]
        /// <summary>
        /// product service rating method
        /// </summary>
        /// <returns>result of the patching action</returns>
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);

            return Ok();
        }

        /// <summary>
        ///  get set method for rating
        /// </summary>
        public class RatingRequest
        {
            public string ProductId { get; set; }
            public int Rating { get; set; }
        }

        [HttpPatch]
        ///<summary>
        /// Product Add comment method
        /// </summary>
        /// <returns>result of the patching action</returns>
        public ActionResult Patch([FromBody] CommentRequest request)
        {
            ProductService.AddComment(request.ProductId, request.Comment);

            return Ok();
        }

        /// <summary>
        /// comment request class
        /// </summary>
        public class CommentRequest
        {
            /// get set method for the product ID
            public string ProductId { get; set; }

            /// get set method for the comment
            public string Comment { get; set; }
        }
    }
}