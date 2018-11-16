using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace EndpointRoutingSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseHealthChecks("/Home/IsUp", new HealthCheckOptions
            {
                ResponseWriter = async (context, health) =>
                {
                    await context.Response.WriteAsync("Mucho Bien");
                }
            });
            app.UseMvcWithDefaultRoute();
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>Health Check - custom message</h1>
                The health check service checks on this url <a href=""/home/isup"">/home.isup</a>. It will replace whatever text you put on that endpoint with the text `Mucho Bien` thanks to the customized health message.
                </body></html> ",
                ContentType = "text/html"
            };
        }

        public ActionResult IsUp()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>Health Check - it's Up</h1>
                </body></html> ",
                ContentType = "text/html"
            };
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}