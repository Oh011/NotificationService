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
    internal class NotificationTopicConf : IEntityTypeConfiguration<NotificationTopic>
    {
        public void Configure(EntityTypeBuilder<NotificationTopic> builder)
        {
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .HasMaxLength(500);

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);


            builder.HasMany(t => t.Notifications)
                   .WithOne(n => n.Topic)
                   .HasForeignKey(n => n.TopicId)
                   .OnDelete(DeleteBehavior.SetNull); // topic deletion won't delete notifications

  
            builder.HasMany(t => t.Subscriptions)
                   .WithOne(s => s.Topic)
                   .HasForeignKey(s => s.TopicId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade); // if topic deleted, remove subscriptions

            // Optional index on Name for search
            builder.HasIndex(t => t.Name).IsUnique();
        }
    }
}
