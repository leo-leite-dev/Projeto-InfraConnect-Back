using InfraConnect.Domain.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraConnect.Infrastructure.Data.Configurations
{
    public class AgentCaseStepConfiguration : IEntityTypeConfiguration<AgentCaseStep>
    {
        public void Configure(EntityTypeBuilder<AgentCaseStep> builder)
        {
            builder.ToTable("AgentCaseSteps");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.PerformedById)
                .IsRequired();

            builder.Property(s => s.PerformedByName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.PerformedByRole)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Action)
                .HasConversion<int>() 
                .IsRequired();

            builder.Property(s => s.Description)
                .HasMaxLength(2000);

            builder.Property(s => s.PerformedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(s => s.AgentCase)
                .WithMany(c => c.Steps)
                .HasForeignKey(s => s.AgentCaseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}