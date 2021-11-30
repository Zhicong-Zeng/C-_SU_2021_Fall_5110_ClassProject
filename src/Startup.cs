using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContosoCrafts.WebSite
{   
    /// <summary>
    /// Entry point for our application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Defautl constructor
        /// </summary>
        /// <param name = "configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // get method for the congiguration
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Adds services for pages to the specified IServiceCollection.
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddControllers();
            services.AddTransient<JsonFileProductService>();
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Landing", "");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Checks if the current hosting environment name is Development.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Checks if the current hosting environment name is Development. If no, return to Error Page
            if (env.IsDevelopment() == false)
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Adds middleware for redirecting HTTP Requests to HTTPS.
            app.UseHttpsRedirection();

            //Enables static file serving for the current request path
            app.UseStaticFiles();

            //Adds a Microsoft.AspNetCore.Routing.EndpointRoutingMiddleware middleware to the specified IApplicationBuilder.
            app.UseRouting();

            //Adds the AuthorizationMiddleware to the specified IApplicationBuilder, which enables authorization capabilities.
            app.UseAuthorization();

            //Adds the AuthorizationMiddleware to the specified IApplicationBuilder, which enables authorization capabilities.
            app.UseAuthentication();

            //Adds the AuthorizationMiddleware to the specified IApplicationBuilder, which enables authorization capabilities.
            app.UseAuthentication();

            //Adds a Microsoft.AspNetCore.Routing.EndpointMiddleware middleware to the specified IApplicationBuilder 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();

            });
        }
    }
}