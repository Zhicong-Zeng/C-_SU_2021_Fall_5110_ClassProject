using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Index Page will return all the data to show
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name = "productService"></param>
        public IndexModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Data Service
        public JsonFileProductService ProductService { get; }
        
        // Collection of the Data
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// REST OnGet, return all data or sorted data if parameter is supplied
        /// </summary>
        /// <param name="sortOrder"
        public void OnGet(string sortOrder)
        {
            // if a sortOrder parameter is supplied, sort the product list.
            // otherwise, return unsorted products
            Products = sortOrder switch
            {
                "title_asc" => ProductService.GetProductSortedByAscTitle(),
                "title_desc" => ProductService.GetProductSortedByDescTitle(),
                "artist_asc" => ProductService.GetProductSortedByAscArtist(),
                "artist_desc" => ProductService.GetProductSortedByDescArtist(),
                "rating_desc" => ProductService.GetProductSortedByDescRating(),
                "rating_asc" => ProductService.GetProductSortedByAscRating(),
                _ => ProductService.GetProducts(),
            };
        }
    }
}