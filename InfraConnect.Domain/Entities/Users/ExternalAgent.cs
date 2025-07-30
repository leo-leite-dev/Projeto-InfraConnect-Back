using InfraConnect.Domain.Enums;
using InfraConnect.Domain.Exceptions;

namespace InfraConnect.Domain.Entities.Users
{
    public class ExternalAgent : UserBase
    {
        public string FullName { get; private set; } = string.Empty;
        public string Company { get; private set; } = string.Empty;
        public string JobTitle { get; private set; } = string.Empty;

        public string? Phone { get; private set; }
        public DateTime? AccessExpiresAt { get; private set; }

        public UserExternalRole ExternalRole { get; private set; } = UserExternalRole.ExternalAgent;

        private ExternalAgent() { }

        public ExternalAgent(string fullName, string email, string passwordHash,
                             string company, string jobTitle, string? phone = null,
                             DateTime? accessExpiresAt = null,
                             UserExternalRole role = UserExternalRole.ExternalAgent,
                             string? username = null)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 200)
                throw new UserException("O nome completo deve ter entre 3 e 200 caracteres.");

            if (string.IsNullOrWhiteSpace(company))
                throw new UserException("Empresa é obrigatória.");

            if (string.IsNullOrWhiteSpace(jobTitle))
                throw new UserException("Cargo é obrigatório.");

            FullName = fullName.Trim();
            Company = company.Trim();
            JobTitle = jobTitle.Trim();
            Phone = phone?.Trim();
            AccessExpiresAt = accessExpiresAt;
            ExternalRole = role;

            InitializeBase(email, passwordHash, username);
        }
    }
}