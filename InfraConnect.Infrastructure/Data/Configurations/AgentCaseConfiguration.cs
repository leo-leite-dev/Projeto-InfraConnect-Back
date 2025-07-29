using InfraConnect.Domain.Entities.Cases.InfraConnect.Domain.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraConnect.Infrastructure.Data.Configurations
{
    public class AgentCaseConfiguration : IEntityTypeConfiguration<AgentCase>
    {
        public void Configure(EntityTypeBuilder<AgentCase> builder)
        {
            builder.ToTable("AgentCases");

            builder.HasKey(ac => ac.Id);

            builder.Property(ac => ac.Subject)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ac => ac.Description)
                .IsRequired();

            builder.Property(ac => ac.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(ac => ac.Priority)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(ac => ac.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(ac => ac.Origin)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ac => ac.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(ac => ac.UpdatedAt)
                .IsRequired(false);

            builder.Property(ac => ac.Deadline)
                .IsRequired(false);

            builder.Property(ac => ac.RequestedExecutionDate)
                .IsRequired(false);

            builder.Property(ac => ac.FinalizedAt)
                .IsRequired(false);

            builder.Property(ac => ac.FinalResultSummary)
                .HasMaxLength(1000);

            builder.HasOne(ac => ac.ExternalAgent)
               .WithMany()
               .HasForeignKey(ac => ac.ExternalAgentId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ac => ac.CreatedByUser)
               .WithMany()
               .HasForeignKey(ac => ac.CreatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ac => ac.CurrentResponsible)
               .WithMany()
               .HasForeignKey(ac => ac.CurrentResponsibleId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(ac => ac.Steps)
               .WithOne(s => s.AgentCase)
               .HasForeignKey(s => s.AgentCaseId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ac => ac.Comments)
               .WithOne(c => c.AgentCase)
               .HasForeignKey(c => c.AgentCaseId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ac => ac.Attachments)
               .WithOne(a => a.AgentCase)
               .HasForeignKey(a => a.AgentCaseId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}