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
    public class UserNotificationPreferenceConfiguration : IEntityTypeConfiguration<UserNotificationPreference>
    {
        public void Configure(EntityTypeBuilder<UserNotificationPreference> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                   .IsRequired();

            builder.HasIndex(p => new { p.UserId, p.CategoryId, p.Channel })
                   .IsUnique(); // ensures a user has only one preference per category/channel

            builder.Property(p => p.IsEnabled)
                   .HasDefaultValue(true);

            builder.HasOne(p => p.NotificationCategory)
                   .WithMany() // we could also have a collection if needed
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
