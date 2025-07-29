using InfraConnect.Domain.Entities.Cases.InfraConnect.Domain.Entities.Cases;
using InfraConnect.Domain.Entities.Commons;
using InfraConnect.Domain.Enums;

namespace InfraConnect.Domain.Entities.Cases
{
    public class AgentCaseStep : Base
    {
        public Guid AgentCaseId { get; set; }
        public AgentCase AgentCase { get; set; } = null!;

        public Guid PerformedById { get; set; }
        public string PerformedByName { get; set; } = string.Empty;
        public string PerformedByRole { get; set; } = string.Empty;

        public StepActionType Action { get; set; }
        public string? Description { get; set; }
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
    }
}