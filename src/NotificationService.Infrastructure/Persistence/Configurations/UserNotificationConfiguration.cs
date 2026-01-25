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
    internal class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            // Primary key
            builder.HasKey(un => un.Id);

            // Properties
            builder.Property(un => un.UserId)
                   .IsRequired()
                   .HasMaxLength(450); // matches AspNetUsers.Id length

            builder.Property(un => un.IsRead)
                   .HasDefaultValue(false);

            builder.Property(un => un.ReadAt)
                   .IsRequired(false);


            builder.HasOne(un => un.Notification)
                   .WithMany() // Notification doesn't need navigation back
                   .HasForeignKey(un => un.NotificationId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(un => un.NotificationChannelStatuses)
                   .WithOne(ncs => ncs.UserNotification)
                   .HasForeignKey(ncs => ncs.UserNotificationId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            // Unique constraint: User + Notification
            builder.HasIndex(un => new { un.UserId, un.NotificationId })
                   .IsUnique();


        }
    }

}
