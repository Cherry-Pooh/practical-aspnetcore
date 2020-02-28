using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");

                var requestUrl = context.Request.GetEncodedPathAndQuery();
                return context.Response.WriteAsync($@"<html>
<body>                
    <h1>Get Encoded Path and Query</h1>
    <i>Returns the relative url</i>
    <a href=""https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.extensions.urihelper.getencodedpathandquery?view=aspnetcore-3.1#Microsoft_AspNetCore_Http_Extensions_UriHelper_GetEncodedPathAndQuery_Microsoft_AspNetCore_Http_HttpRequest_"">Doc</a><br/><br/>
    <p style=""color:red;"">{ requestUrl }</p>

    <p>Click on the links to see what the helper method shows</p>
    <ul>
        <li><a href=""/about/us"">/about/us</a></li>
        <li><a href=""/city/?id=30"">/city/?id=30</a></li>
        <li><a href=""/continent/?id=300&lat=30&lng=87"">/continent/?id=300&lat=30&lng=87</a></li>
        <li><a href=""/person?name=annie"">/person?name=annie</a></li>
        <li><a href=""/president?name=Franklin D. Roosevelt"">/president?name=Franklin D. Roosevelt</a></li>
        <li><a href=""/"">/</a></li>
        <li><a href=""/#header"">/#header</a></li>
    </ul>
</body>          
</html>         ");
            });
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