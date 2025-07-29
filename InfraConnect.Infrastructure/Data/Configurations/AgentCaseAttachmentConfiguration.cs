using InfraConnect.Domain.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraConnect.Infrastructure.Data.Configurations
{
    public class AgentCaseAttachmentConfiguration : IEntityTypeConfiguration<AgentCaseAttachment>
    {
        public void Configure(EntityTypeBuilder<AgentCaseAttachment> builder)
        {
            builder.ToTable("AgentCaseAttachments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.FileSize)
                .IsRequired();

            builder.Property(a => a.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(a => a.AgentCase)
                .WithMany(c => c.Attachments)
                .HasForeignKey(a => a.AgentCaseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}