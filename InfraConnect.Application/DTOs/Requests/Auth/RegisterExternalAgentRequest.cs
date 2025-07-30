using InfraConnect.Domain.Enums;

namespace InfraConnect.Application.DTOs.Requests.Auth
{
    public class RegisterExternalAgentRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
        public string Password { get; set; } = default!;
        public string? PasswordHash { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public DateTime AccessExpiresAt { get; set; }

        public UserExternalRole Role { get; set; } = UserExternalRole.ExternalAgent;
    }
}