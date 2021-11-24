using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Connections;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment environment)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapFallbackToAreaPage("/Other", "Admin");
                endpoints.MapFallbackToAreaPage("{segment}/{segment2}", "/OtherLevel", "Admin");
                endpoints.MapFallbackToAreaPage("{number:int}", "/OtherNumber", "Admin");
            });
        }
    }

}