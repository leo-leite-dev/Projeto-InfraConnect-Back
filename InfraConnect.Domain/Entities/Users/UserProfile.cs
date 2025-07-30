using InfraConnect.Domain.Entities.Commons;
using InfraConnect.Domain.Enums;
using InfraConnect.Domain.Exceptions;

namespace InfraConnect.Domain.Entities.Users
{
    public class UserProfile : Base
    {
        public string FullName { get; private set; }
        public string CPF { get; private set; }
        public string? RG { get; protected set; }
        public DateTime? BirthDate { get; private set; }
        public string? Phone { get; protected set; }

        public Address Address { get; protected set; }
        public Department DepartmentEnum { get; private set; }
        public JobTitle JobTitleEnum { get; private set; }
        public DateTime? AdmissionDate { get; private set; }
        public string? ProfileImageUrl { get; protected set; }

        private UserProfile() { }

        public UserProfile(
            string fullName,
            string cpf,
            Department department,
            JobTitle jobTitle,
            Address address,
            string? rg = null,
            DateTime? birthDate = null,
            string? phone = null,
            DateTime? admissionDate = null,
            string? profileImageUrl = null)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3 || fullName.Length > 200)
                throw new UserException("O nome completo deve ter entre 3 e 200 caracteres.");

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                throw new UserException("CPF inválido.");

            if (birthDate.HasValue)
            {
                var birth = birthDate.Value.Date;

                if (birth > DateTime.UtcNow.Date)
                    throw new UserException("A data de nascimento não pode estar no futuro.");

                if (birth < DateTime.UtcNow.AddYears(-100))
                    throw new UserException("Data de nascimento muito antiga.");

                var age = DateTime.UtcNow.Year - birth.Year;
                if (birth > DateTime.UtcNow.AddYears(-age)) age--;

                if (age < 18)
                    throw new UserException("Usuário deve ter pelo menos 18 anos.");
            }

            if (admissionDate.HasValue && admissionDate.Value.Date > DateTime.UtcNow.Date)
                throw new UserException("A data de admissão não pode estar no futuro.");

            FullName = fullName.Trim();
            CPF = cpf;
            RG = rg?.Trim();
            BirthDate = birthDate;
            Phone = phone?.Trim();
            AdmissionDate = admissionDate;
            ProfileImageUrl = profileImageUrl?.Trim();
            DepartmentEnum = department;
            JobTitleEnum = jobTitle;
            Address = address ?? throw new UserException("Endereço é obrigatório.");
        }

        // (Opcional) Método auxiliar para exibir CPF formatado
        public string GetFormattedCpf()
        {
            if (CPF.Length != 11)
                return CPF;

            return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
        }
    }
}
