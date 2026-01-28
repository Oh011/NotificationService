using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using NotificationService.Api.SwaggerOptions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NotificationService.Api.Extensions
{
    public static  class PresentationExtensions
    {


        public static IServiceCollection AddPresenation(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer(); // for minimal APIs & OpenAPI

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                //This tells ASP.NET Core to accept and serialize enum values as strings.

                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;


                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;





            });




            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true; //--> If a request doesn't specify a version, use the default.
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader(); // use URL path versioning

            });



            ConfigureSwaggerOptions(services);





            return services;
        }



        private static void ConfigureSwaggerOptions(IServiceCollection Services)
        {

            Services.AddSwaggerGen(c =>
            {



                // Include XML comments (see next step) --> to enable Xml Documents
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Add JWT Auth support
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer {your JWT token}'"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                            }
                    });


            });

            Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; // Format: v1, v2, etc.
                options.SubstituteApiVersionInUrl = true;
            });



            Services.ConfigureOptions<ConfigureSwaggerOptions>();

            Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; // Format: v1, v2, etc.
                options.SubstituteApiVersionInUrl = true;
            });

        }
    }
}
