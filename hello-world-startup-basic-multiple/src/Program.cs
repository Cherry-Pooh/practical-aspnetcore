using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace StartupBasic
{
    public class Startup1
    {
        public Startup1(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world from Startup1");
            });
        }
    }

    public class Startup2
    {
        public Startup2(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world Startup2");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
              .UseKestrel();

            //This is a dumb way of doing it. You can use command line argument, etc to switch your startup 
            const int startupNumber = 1; //CHANGE THIS to 2 if you want to use Startup2

            if (startupNumber == 1)
                host.UseStartup<Startup1>();
            else if (startupNumber == 2)
                host.UseStartup<Startup2>();

            host.Build().Run();
        }
    }
}