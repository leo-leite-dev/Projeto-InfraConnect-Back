using InfraConnect.Domain.Entities.Commons;
using InfraConnect.Domain.Entities.Users;
using InfraConnect.Domain.Enums;

namespace InfraConnect.Domain.Entities.Cases
{
    public class AgentCase : Base
    {
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AgentCaseType Type { get; set; } = AgentCaseType.Other;
        public CasePriority Priority { get; set; } = CasePriority.Normal;
        public AgentCaseStatus Status { get; set; } = AgentCaseStatus.Pending;

        public string Origin { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? RequestedExecutionDate { get; set; }
        public DateTime? FinalizedAt { get; set; }

        public Guid ExternalAgentId { get; set; }
        public ExternalAgent ExternalAgent { get; set; } = null!;

        public Guid CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;

        public Guid? CurrentResponsibleId { get; set; }
        public User? CurrentResponsible { get; set; }

        public string? FinalResultSummary { get; set; }

        public List<AgentCaseStep> Steps { get; set; } = new();
        public List<AgentCaseComment> Comments { get; set; } = new();
        public List<AgentCaseAttachment> Attachments { get; set; } = new();
    }
}