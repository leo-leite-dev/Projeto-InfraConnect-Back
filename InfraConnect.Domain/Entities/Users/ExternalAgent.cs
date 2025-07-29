using InfraConnect.Domain.Entities.Commons;
using InfraConnect.Domain.Enums;

namespace InfraConnect.Domain.Entities.Users
{
    public class ExternalAgent : Base
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public DateTime? AccessExpiresAt { get; set; }

        public UserExternalRole Role { get; set; } = UserExternalRole.ExternalAgent;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}