using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.AddRouting();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "home",
                    template: "/",
                    defaults: new { controller = "HomePage", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}",
                    defaults: new { controller = "HomePage", action = "Index" });

            });
        }
    }

    public class HomePageController : Controller
    {
        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <b>Hello World</b>
                <br/><br/>
                The following links call the same controller and action.
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/homepage"">/homepage</a></li>
                    <li><a href=""/homepage/index"">/homepage/index</a></li>
                </ul>
                </body></html>",
                ContentType = "text/html"
            };
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}