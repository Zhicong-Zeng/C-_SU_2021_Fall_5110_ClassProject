using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    /// <summary>
    /// Error handling class
    /// </summary>
    public class ErrorModel : PageModel
    {
        // Get and set method for the request ID
        public string RequestId { get; set; }

        // Checks if the Request ID is NULL or empty
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Creates a private readonly ILogger for the ErrorModel
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Onget method for the Request ID
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}