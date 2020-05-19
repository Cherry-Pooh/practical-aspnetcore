using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Grpc.Core;
using System;
using Microsoft.Extensions.DependencyInjection;
using Billboard;
using System.Linq;

namespace GrpcServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseGrpcWeb();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<BillboardService>().EnableGrpcWeb().RequireCors("AllowAll");
                endpoints.MapGet("/", context =>
                {
                    return context.Response.WriteAsync("This server contains a gRPCWeb service");
                });
            });
        }
    }

    public class BillboardService : Billboard.Board.BoardBase
    {
        public override async Task ShowMessage(MessageRequest request, IServerStreamWriter<MessageReply> responseStream, ServerCallContext context)
        {
            foreach (var x in Enumerable.Range(1, 10))
            {
                var now = DateTime.UtcNow;

                await responseStream.WriteAsync(new Billboard.MessageReply
                {
                    DisplayTime = now.Ticks,
                    Message = $"Hello {request.Name}"
                });

                await Task.Delay(5000);
            }
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
                {
                    webBuilder.UseStartup<Startup>().
                    ConfigureKestrel(k =>
                    {
                        k.ListenLocalhost(5500);
                    });
                });
    }
}