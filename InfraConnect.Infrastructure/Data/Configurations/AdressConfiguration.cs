using InfraConnect.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraConnect.Infrastructure.Data.Configurations
{
    public class AdressConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.OwnsOne(p => p.Address, address =>
            {
                address.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasMaxLength(200);

                address.Property(a => a.Number)
                    .HasColumnName("Number")
                    .HasMaxLength(20);

                address.Property(a => a.Complement)
                    .HasColumnName("Complement")
                    .HasMaxLength(100);

                address.Property(a => a.Neighborhood)
                    .HasColumnName("Neighborhood")
                    .HasMaxLength(100);

                address.Property(a => a.City)
                    .HasColumnName("City")
                    .HasMaxLength(100);

                address.Property(a => a.State)
                    .HasColumnName("State")
                    .HasMaxLength(2);

                address.Property(a => a.ZipCode)
                    .HasColumnName("ZipCode")
                    .HasMaxLength(10);
            });
        }
    }
}
