using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Manage the Delete of the data for a single record
    /// </summary>
    public class DeleteModel : PageModel
    {
        // Data middletier
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Defualt Construtor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // The data to show, bind to it for the post
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// REST Get request
        /// Loads the Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result of the get action</returns>
        public IActionResult OnGet(string id)
        {
            //Get the id data and check it whether null
            Product  = ProductService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));

            //if data is null ,return to index.page
            if (Product == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        /// <summary>
        /// Post the model back to the page
        /// The model is in the class variable Product
        /// Call the data layer to Delete that data
        /// Then return to the index page
        /// </summary>
        /// <returns>Result of the post action</returns>
        public IActionResult OnPost()
        {
            //Call the data layer to Delete that data
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Delete that data
            ProductService.DeleteData(Product.Id);

            //return to index.page
            return RedirectToPage("./Index");
        }
    }
}