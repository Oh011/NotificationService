
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using NotificationService.Api.Extensions;
using NotificationService.Api.Middlewares;
using NotificationService.Api.SwaggerOptions;
using NotificationService.Infrastructure.DependencyInjection;
using System.Reflection;
using System.Threading.Tasks;

namespace NotificationService.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          

          
            builder.Services.AddPresenation();  


            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddOpenApi();

            





            var app = builder.Build();


            app.UseMiddleware<GlobalErrorHandleMiddleware>();   

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();       // Serves swagger.json



                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                                $"Notification Service API {description.GroupName.ToUpperInvariant()}");
                    }

                    //options.RoutePrefix = string.Empty; // Swagger at root
                });

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

           await app.InitializeDatabaseAsync();

            app.Run();
        }
    }
}
