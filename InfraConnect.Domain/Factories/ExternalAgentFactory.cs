using InfraConnect.Domain.Entities.Users;
using InfraConnect.Domain.Enums;

namespace InfraConnect.Domain.Factories
{
    public static class ExternalAgentFactory
    {
        public static ExternalAgent Create(
            string fullName,
            string email,
            string rawPassword,
            string company,
            string jobTitle,
            string? phone,
            DateTime? accessExpiresAt,
            UserExternalRole role,
            string? username,
            Func<string, string> hashFunction)
        {
            var agent = new ExternalAgent(
                fullName.Trim(),
                email.Trim().ToLower(),
                company.Trim(),
                jobTitle.Trim(),
                phone?.Trim(),
                accessExpiresAt,
                role,
                username?.Trim()
            );

            agent.SetPassword(rawPassword, hashFunction);
            return agent;
        }
    }
}
