using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Persistence.Configurations
{
    internal class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            

            builder.ToTable("RefreshTokens");

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(256);


            builder.Property(rt => rt.DeviceId)
                   .IsRequired()
                   .HasMaxLength(200); // reasonable length for device identifiers

            builder.Property(rt => rt.UserId)
                   .IsRequired();

            builder.Property(rt => rt.CreatedAt)
                   .IsRequired();

            builder.Property(rt => rt.ExpiresAt)
                   .IsRequired();

            builder.HasIndex(rt => rt.Token)
       .IsUnique();

            // Indexes for frequent searches


            builder.HasIndex(rt => new { rt.UserId, rt.DeviceId });

            builder.HasIndex(rt => rt.UserId);

            // Relationships
            builder.HasOne(rt => rt.User)
                   .WithMany() // or .WithMany(u => u.RefreshTokens) if you add navigation in ApplicationUser
                   .HasForeignKey(rt => rt.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
