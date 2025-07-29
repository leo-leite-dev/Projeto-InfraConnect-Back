using InfraConnect.Domain.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraConnect.Infrastructure.Data.Configurations
{
    public class AgentCaseCommentConfiguration : IEntityTypeConfiguration<AgentCaseComment>
    {
        public void Configure(EntityTypeBuilder<AgentCaseComment> builder)
        {
            builder.ToTable("AgentCaseComments");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(2000); 

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(c => c.AuthorId)
                .IsRequired();

            builder.Property(c => c.AuthorName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.AuthorRole)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(c => c.AgentCase)
                .WithMany(ac => ac.Comments)
                .HasForeignKey(c => c.AgentCaseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}