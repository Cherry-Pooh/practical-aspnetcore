using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HelloWorldWithReload 
{
    public class DebuggerRouteHandler : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public Task RouteAsync(RouteContext context)
        {
            var routeValues = context.HttpContext.GetRouteData().Values;
            return context.HttpContext.Response.WriteAsync($"Hello {routeValues["name"]}");
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var defaultHandler = new RouteHandler (context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Route values: {string.Join(", ", routeValues)}");
            });

            var routes = new RouteBuilder(app, defaultHandler);
            
            routes.MapVerb("GET", "hello", (IApplicationBuilder app2) =>{
                var routes2 = new RouteBuilder(app2);
                routes2.MapGet("callme", (context) => {
                    return context.Response.WriteAsync("call me");
                });

                routes2.MapGet("{name}", context => {
                    var routeValues = context.GetRouteData().Values;
                    return context.Response.WriteAsync($"hello {routeValues["name"]}");
                });

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
              var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}