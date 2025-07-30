using InfraConnect.Domain.Entities.Commons;

namespace InfraConnect.Domain.Entities.Cases
{
    public class AgentCaseComment : Base
    {
        public Guid AgentCaseId { get; set; }
        public AgentCase AgentCase { get; set; } = null!;

        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorRole { get; set; } = string.Empty;
    }
}