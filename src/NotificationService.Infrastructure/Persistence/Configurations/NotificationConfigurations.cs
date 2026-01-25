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
    internal class NotificationConfigurations : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
           

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title)
                .IsRequired().HasMaxLength(200);


            builder.Property(n => n.Body).IsRequired().HasColumnType("Varchar(max)");




            builder.Property(n => n.Channels)
            .HasConversion(
                c => c.ToString(),                          // Enum to string
                s => (NotificationChannel)Enum.Parse(typeof(NotificationChannel), s))
            .IsRequired();
            // string to Enum


            builder.HasOne(n => n.Category)
                .WithMany(c => c.Notifications)
                .HasForeignKey(n => n.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(n => n.Topic)
          .WithMany(t => t.Notifications) 
          .HasForeignKey(n => n.TopicId)
          .OnDelete(DeleteBehavior.SetNull);

            // RecipientUserId optional, no FK to ApplicationUser (clean arch style)
            builder.Property(n => n.RecipientUserId)
                .HasMaxLength(450); // matches AspNetUsers.Id length


            builder.HasIndex(n => n.RecipientUserId);

        }
    }
}
