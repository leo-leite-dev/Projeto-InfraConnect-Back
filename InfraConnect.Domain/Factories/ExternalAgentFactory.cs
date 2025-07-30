using InfraConnect.Domain.Entities.Users;
using InfraConnect.Domain.Enums;

namespace InfraConnect.Domain.Factories
{
    public static class ExternalAgentFactory
    {
        public static ExternalAgent Create(
            string fullName,
            string email,
            string passwordHash,
            string company,
            string jobTitle,
            string? phone = null,
            DateTime? accessExpiresAt = null,
            UserExternalRole role = UserExternalRole.ExternalAgent,
            string? username = null)
        {
            return new ExternalAgent(
                fullName.Trim(),
                email.Trim().ToLower(),
                passwordHash,
                company.Trim(),
                jobTitle.Trim(),
                phone?.Trim(),
                accessExpiresAt,
                role,
                username?.Trim()
            );
        }
    }
}
