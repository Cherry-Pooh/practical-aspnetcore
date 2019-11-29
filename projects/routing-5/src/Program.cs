using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.AspNetCore;

namespace Routing5
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IInlineConstraintResolver resolver)
        {
            var defaultHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Route values: {string.Join(", ", routeValues)}");
            });

            var routes = new RouteBuilder(app, defaultHandler);

            routes.MapMiddlewareGet("hello", (IApplicationBuilder app2) =>
            {
                var routes2 = new RouteBuilder(app2);
                routes2.MapGet("world", (context) => context.Response.WriteAsync("Hello World"));
                app2.UseRouter(routes2.Build());
            });

            routes.MapRoute(
                name: "Default",
                template: "{*path}"
            );

            IRouter routing = routes.Build();
            app.UseRouter(routing);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}