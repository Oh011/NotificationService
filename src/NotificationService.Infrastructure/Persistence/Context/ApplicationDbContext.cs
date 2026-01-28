using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Persistence.Context
{
    internal class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");


            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


        public DbSet<Notification> Notifications { get; set; }


       public DbSet<RefreshToken> RefreshTokens { get; set; }   
        public DbSet<NotificationChannelStatus> NotificationChannelStatuses { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }


        public DbSet<NotificationCategory> NotificationCategories { get; set; }


        public DbSet<NotificationTopic> NotificationTopics { get; set; }


        public DbSet<UserNotificationPreference> UserNotificationPreferences { get; set; }



        public DbSet<UserNotificationSubscription> UserNotificationSubscriptions { get; set; }



        }
}
