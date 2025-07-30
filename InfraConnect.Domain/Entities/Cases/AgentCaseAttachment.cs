using InfraConnect.Domain.Entities.Commons;

namespace InfraConnect.Domain.Entities.Cases
{
    public class AgentCaseAttachment : Base
    {
        public Guid AgentCaseId { get; set; }
        public AgentCase AgentCase { get; set; } = null!;

        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}