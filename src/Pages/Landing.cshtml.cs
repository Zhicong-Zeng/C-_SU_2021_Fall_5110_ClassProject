using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Backend class for Landing.cshtml
    /// </summary>
    public class LandingModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        public LandingModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Gets ProductService
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Iterable IEnumerable of Products
        /// </summary>
        public IEnumerable<ProductModel> Products { get;}

        /// <summary>
        /// Gets, sets List of TopThreeArtworks
        /// </summary>
        public List<ProductModel> TopThreeArtwork { get; private set; }

        /// <summary>
        /// OnGet gets top 3 highest rated artworks from ProductService
        /// </summary>
        public void OnGet()
        {
            TopThreeArtwork = ProductService.GetHighestRatedArtwork();
        }
    }
}
