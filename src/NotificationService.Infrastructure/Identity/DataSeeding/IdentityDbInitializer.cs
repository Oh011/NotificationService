using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationService.Infrastructure.Identity.Models;
using NotificationService.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Identity.DataSeeding
{
    internal class IdentityDbInitializer(ApplicationDbContext context,
       RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager
        ,ILogger<IIdentityDbInitializer> logger) : IIdentityDbInitializer
    {
        public async Task InitilaizeAssync()
        {



            try
            {


            if (context.Database.GetPendingMigrations().Any())
                await context.Database.MigrateAsync();


            if (!await roleManager.Roles.AnyAsync())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole("Admin"),
                    new IdentityRole("User")
                };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }


            if (!await userManager.Users.AnyAsync())
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "Admin@Gamil.com"

                };


                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                }

                else
                {
                  
                    foreach (var error in result.Errors)
                    {
                        logger.LogError("Error creating admin user: {Error}", error.Description);
                    }
                    
                }


            }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initializing the identity database.");
                throw;
            }



        }

    }
}
