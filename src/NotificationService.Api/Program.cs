
using NotificationService.Api.Extensions;
using NotificationService.Api.Middlewares;
using NotificationService.Infrastructure.DependencyInjection;
using System.Threading.Tasks;

namespace NotificationService.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddOpenApi();

            var app = builder.Build();


            app.UseMiddleware<GlobalErrorHandleMiddleware>();   

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

           await app.InitializeDatabaseAsync();

            app.Run();
        }
    }
}
