using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Model for the Home Page
    /// </summary>
    public class HomeModel : PageModel
    {
        // Creates a private readonly Ilogger for HomeModel
        private readonly ILogger<HomeModel> _logger;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public HomeModel(ILogger<HomeModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        // Retrieves Product Service
        public JsonFileProductService ProductService { get; }

        // Retreive Products and privately sets 
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// Retrieves Products using razor component
        /// </summary>
        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}