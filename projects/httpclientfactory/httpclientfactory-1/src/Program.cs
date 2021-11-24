using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var httpClient = context.RequestServices.GetService<IHttpClientFactory>();
                var client = httpClient.CreateClient();
                var result = await client.GetStringAsync("http://scripting.com/rss.xml");

                context.Response.Headers.Add("Content-Type", "application/rss+xml");
                await context.Response.WriteAsync(result);
            });
        }
    }



}