using InfraConnect.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InfraConnect.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.CPF)
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(u => u.RG)
                .HasMaxLength(20);

            builder.Property(u => u.Phone)
                .HasMaxLength(20);

            builder.OwnsOne(u => u.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("Street").HasMaxLength(200);
                address.Property(a => a.Number).HasColumnName("Number").HasMaxLength(20);
                address.Property(a => a.Complement).HasColumnName("Complement").HasMaxLength(100);
                address.Property(a => a.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(100);
                address.Property(a => a.City).HasColumnName("City").HasMaxLength(100);
                address.Property(a => a.State).HasColumnName("State").HasMaxLength(2);
                address.Property(a => a.ZipCode).HasColumnName("ZipCode").HasMaxLength(10);
            });

            builder.Property(u => u.DepartmentEnum)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(u => u.JobTitleEnum)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(u => u.UpdatedAt)
                .IsRequired(false);
        }
    }
}