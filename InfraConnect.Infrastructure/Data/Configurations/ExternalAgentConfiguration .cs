using InfraConnect.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraConnect.Infrastructure.Data.Configurations
{
    public class ExternalAgentConfiguration : IEntityTypeConfiguration<ExternalAgent>
    {
        public void Configure(EntityTypeBuilder<ExternalAgent> builder)
        {
            builder.ToTable("ExternalAgents");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.Company)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.JobTitle)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Phone)
                .HasMaxLength(20);

            builder.Property(e => e.AccessExpiresAt)
                .IsRequired(false);

            builder.Property(e => e.ExternalRole)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.Username).IsUnique();
        }
    }
}