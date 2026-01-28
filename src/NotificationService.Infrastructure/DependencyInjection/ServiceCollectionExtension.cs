using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Abstractions.Security;
using NotificationService.Infrastructure.Identity;
using NotificationService.Infrastructure.Identity.DataSeeding;
using NotificationService.Infrastructure.Identity.Models;
using NotificationService.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtension
    {


        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddScoped<IIdentityDbInitializer, IdentityDbInitializer>();


            services.AddScoped<IAuthenticationService, AuthenticationService>();  

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });



            services.AddIdentity<ApplicationUser,IdentityRole>(options => {



                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                options.Tokens.EmailConfirmationTokenProvider = "Default";



                // Lockout settings (optional for security)
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Sign-in settings




            }).AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();

            return services;
        }
    }
}
