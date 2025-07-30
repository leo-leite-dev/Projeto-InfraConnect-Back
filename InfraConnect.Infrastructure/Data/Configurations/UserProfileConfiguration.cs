using InfraConnect.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraConnect.Infrastructure.Data.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.CPF)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(p => p.RG)
                .HasMaxLength(20);

            builder.Property(p => p.Phone)
                .HasMaxLength(20);

            builder.Property(p => p.BirthDate)
                .IsRequired(false);

            builder.Property(p => p.AdmissionDate)
                .IsRequired(false);

            builder.Property(p => p.ProfileImageUrl)
                .HasMaxLength(300);

            builder.Property(p => p.DepartmentEnum)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.JobTitleEnum)
                .IsRequired()
                .HasConversion<int>();

            builder.OwnsOne(p => p.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(200);
                address.Property(a => a.Number).HasColumnName("Number").HasMaxLength(20);
                address.Property(a => a.Complement).HasColumnName("Complement").HasMaxLength(100);
                address.Property(a => a.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(100);
                address.Property(a => a.City).HasColumnName("City").HasMaxLength(100);
                address.Property(a => a.State).HasColumnName("State").HasMaxLength(2);
                address.Property(a => a.ZipCode).HasColumnName("ZipCode").HasMaxLength(10);
            });
        }
    }
}