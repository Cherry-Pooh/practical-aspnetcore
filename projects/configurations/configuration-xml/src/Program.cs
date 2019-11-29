using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore;

namespace Configuration.Xml 
{
    public class Startup
    {
        IConfigurationRoot _config;

        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //This is the most basic configuration you can have
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("settings.xml");

            _config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.Run(async (context) =>
            {
                foreach(var c in _config.AsEnumerable())
                {
                    await context.Response.WriteAsync($"{c.Key} = {_config[c.Key]}\n");
                }
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