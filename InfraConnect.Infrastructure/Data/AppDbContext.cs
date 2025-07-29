using InfraConnect.Domain.Entities.Cases;
using InfraConnect.Domain.Entities.Cases.InfraConnect.Domain.Entities.Cases;
using InfraConnect.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace InfraConnect.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ExternalAgent> ExternalAgents { get; set; }
        public DbSet<AgentCase> AgentCases { get; set; }
        public DbSet<AgentCaseAttachment> AgentCaseAttachments { get; set; }
        public DbSet<AgentCaseComment> AgentCaseComments { get; set; }
        public DbSet<AgentCaseStep> AgentCaseSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}