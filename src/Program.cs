using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ContosoCrafts.WebSite
{
    /// <summary>
    /// Starting point of the application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// main function of the class creates a host using the instance of the IHostBuilder
        /// </summary>
        /// <param name = "args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// initializes IHostBuilder using CreateDefaultBuilder
        /// </summary>
        /// <param name="args">parameters to define HostBuilder</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}