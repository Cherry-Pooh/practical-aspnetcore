using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing.Template;
using System.Linq;
using System;

namespace Routing7 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IInlineConstraintResolver resolver)
        {
            //We are building a url template from scratch, segment by segemtn, oldskool 
            var segment = new TemplateSegment();
            segment.Parts.Add(
                TemplatePart.CreateLiteral("hello")
            );

            var segment2 = new TemplateSegment();
            segment2.Parts.Add(
                TemplatePart.CreateLiteral("world")
            );

            var segments = new TemplateSegment [] {
                segment,
                segment2
            };

            var template = new RouteTemplate("hello", segments.ToList());
            var templateMatcher = new TemplateMatcher(template, new RouteValueDictionary());

            app.Use(async (context, next) =>{
                var path1 = "hello/world";
                try
                {
                    var isMatch1 = templateMatcher.TryMatch(path1, new RouteValueDictionary());
                    await context.Response.WriteAsync($"{path1} is match? {isMatch1}\n");
                }
                catch(Exception ex){
                    await context.Response.WriteAsync($"Oops {path1}: {ex?.Message}\n\n");
                }
                finally
                {
                    await next.Invoke();
                }
            });

            app.Use(async (context, next) => {
                var path1 = "/hello/world";
                var isMatch1 = templateMatcher.TryMatch(path1, new RouteValueDictionary());
                await context.Response.WriteAsync($"{path1} is match? {isMatch1}\n");
                await next.Invoke();    
            });

            app.Use(async (context, next) => {
                var path1 = "/hello/";
                var isMatch1 = templateMatcher.TryMatch(path1, new RouteValueDictionary());
                await context.Response.WriteAsync($"{path1} is match? {isMatch1}\n");
                await next.Invoke();    
            });
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