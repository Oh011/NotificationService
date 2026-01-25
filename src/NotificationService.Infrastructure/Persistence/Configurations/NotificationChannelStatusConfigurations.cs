using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Persistence.Configurations
{
    internal class NotificationChannelStatusConfigurations
    : IEntityTypeConfiguration<NotificationChannelStatus>
    {
        public void Configure(EntityTypeBuilder<NotificationChannelStatus> builder)
        {
            
            builder.HasKey(ncs => ncs.Id);

            
            builder.HasOne(ncs => ncs.UserNotification)
                   .WithMany(un => un.NotificationChannelStatuses)
                   .HasForeignKey(ncs => ncs.UserNotificationId)
                   .OnDelete(DeleteBehavior.Cascade); // deleting user notification deletes its statuses


            builder.Property(ncs => ncs.Channel)
                   .HasConversion(
                        c => c.ToString(),
                        c => Enum.Parse<NotificationChannel>(c));

            builder.Property(ncs => ncs.Status)
                   .HasConversion(
                        s => s.ToString(),
                        s => Enum.Parse<ChannelDeliveryStatus>(s));

            // Unique constraint on (UserNotificationId, Channel)
            builder.HasIndex(ncs => new { ncs.UserNotificationId, ncs.Channel })
                   .IsUnique();


            builder.Property(ncs => ncs.FailureReason)
                   .HasMaxLength(500);
        }
    }
}

