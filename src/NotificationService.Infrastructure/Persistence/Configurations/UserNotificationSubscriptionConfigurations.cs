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
    public class UserNotificationSubscriptionConfiguration : IEntityTypeConfiguration<UserNotificationSubscription>
    {
        public void Configure(EntityTypeBuilder<UserNotificationSubscription> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.UserId)
                   .IsRequired();

            builder.Property(s => s.SubscribedAt)
                   .IsRequired();

            builder.HasIndex(s => new { s.UserId, s.TopicId })
                   .IsUnique(); // ensures one subscription per user per topic

            builder.HasOne(s => s.Topic)
                   .WithMany(t => t.Subscriptions)
                   .HasForeignKey(s => s.TopicId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
