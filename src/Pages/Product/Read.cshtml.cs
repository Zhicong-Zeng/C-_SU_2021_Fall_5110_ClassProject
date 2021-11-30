using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;


namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Model for Read Page
    /// </summary>
    public class ReadModel : PageModel
    {
        // Comment variable required and max length
        [BindProperty]
        public string Comment { get; set; }

        // The data to show, bind to it for the post
        [BindProperty]
        public ProductModel Product { get; set; }

        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name = "logger"></param>
        /// <param name = "productService"></param>
        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// REST Get request
        /// </summary>
        /// <param name = "id"></param>
        /// <returns>Result of the get action</returns>
        public IActionResult OnGet(string id)
        {
            //Get the data with id
            Product = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));

            //if the data is null, return to index.page
            if (Product == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        /// <summary>
        /// REST Post request to add a comment
        /// </summary>
        /// <returns></returns>
        /// <returns>Result of the post action</returns>
        public IActionResult OnPost()
        {
            //var comment = Request.Form["comment"];
            if (Comment == null)
            {
                return RedirectToPage("./Read");
            }

            //check the comment's length, if less than 0, return to read.page
            if (Comment.Length <= 0)
            {
                return RedirectToPage("./Read");
            }

            //check the comment's length, if more than 250, return to read.page
            if (Comment.Length > 250)
            {
                return RedirectToPage("./Read");
            }

            //Add the comment into storage
            ProductService.AddComment(Product.Id, Comment);

            //Return to Read.page
            return RedirectToPage("./Read");
        }
    }
}