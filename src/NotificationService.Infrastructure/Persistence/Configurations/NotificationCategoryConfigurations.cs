using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Persistence.Configurations
{
    internal class NotificationCategoryConfigurations : IEntityTypeConfiguration<NotificationCategory>
    {
        public void Configure(EntityTypeBuilder<NotificationCategory> builder)
        {
            
            builder.HasKey(c=> c.Id);   

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100); 


            builder.Property(c => c.Description)
                .HasMaxLength(500).IsRequired();


            builder.HasMany(c => c.UserNotificationPreferences)
                .WithOne(p => p.NotificationCategory)
                .IsRequired()
                .HasForeignKey(p => p.CategoryId)
        
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(c => c.Notifications)
                .WithOne(n => n.Category)
                .HasForeignKey(n => n.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
