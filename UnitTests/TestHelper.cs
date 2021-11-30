using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Controllers;

namespace UnitTests
{
    /// <summary>
    /// Helper to hold the web start settings
    ///
    /// HttpClient
    /// 
    /// Action Contect
    /// 
    /// The View Data and Teamp Data
    /// 
    /// The Product Service
    /// </summary>
    public static class TestHelper
    {
        //Create static Default Constructor
        public static Mock<IWebHostEnvironment> MockWebHostEnvironment;
        public static IUrlHelperFactory UrlHelperFactory;
        public static DefaultHttpContext HttpContextDefault;
        public static IWebHostEnvironment WebHostEnvironment;
        public static ModelStateDictionary ModelState;
        public static ActionContext ActionContext;
        public static EmptyModelMetadataProvider ModelMetadataProvider;
        public static ViewDataDictionary ViewData;
        public static TempDataDictionary TempData;
        public static PageContext PageContext;
        public static JsonFileProductService ProductService;
        public static ProductsController ProductController;
        public static ProductsController.CommentRequest CommentRequest;
        public static ProductsController.RatingRequest RatingRequest;

        /// <summary>
        /// Default Constructor
        /// </summary>
        static TestHelper()
        {
            // Initializes an instance of the mock with IWebHostEnvironment and set up the mocked type
            MockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            MockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            MockWebHostEnvironment.Setup(m => m.WebRootPath).Returns(TestFixture.DataWebRootPath);
            MockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(TestFixture.DataContentRootPath);

            // Initializes an instance of DefaultHttpContext and set up
            HttpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
            };
            HttpContextDefault.HttpContext.TraceIdentifier = "trace";

            // Initializes an instance of ModelStateDictionary
            ModelState = new ModelStateDictionary();

            // Initializes an instance of ActionContext and set up
            ActionContext = new ActionContext(HttpContextDefault, HttpContextDefault.GetRouteData(), new PageActionDescriptor(), ModelState);

            // Initializes an instance of EmptyModelMetadataProvider
            ModelMetadataProvider = new EmptyModelMetadataProvider();

            // Initializes an instance of ViewDataDictionary and set up 
            ViewData = new ViewDataDictionary(ModelMetadataProvider, ModelState);

            // Initializes an instance of TempDataDictionary and set up
            TempData = new TempDataDictionary(HttpContextDefault, Mock.Of<ITempDataProvider>());

            // Initializes an instance of PageContext and set up 
            PageContext = new PageContext(ActionContext)
            {
                ViewData = ViewData,
                HttpContext = HttpContextDefault
            };

            // Initializes JsonFileProductService and set up 
            ProductService = new JsonFileProductService(MockWebHostEnvironment.Object);
            JsonFileProductService productService;
            productService = new JsonFileProductService(TestHelper.MockWebHostEnvironment.Object);

            // Creates ProductsController for use in TestHelper
            // Also serves as test for ProductsController(ContosoCrafts.WebSite.Services.JsonFileProductService)
            ProductController = new ProductsController(TestHelper.ProductService);
            CommentRequest = new ProductsController.CommentRequest();
            RatingRequest = new ProductsController.RatingRequest();
        }
    }
}