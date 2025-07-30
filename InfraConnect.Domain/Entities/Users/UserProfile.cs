using InfraConnect.Domain.Entities.Commons;
using InfraConnect.Domain.Enums;
using InfraConnect.Domain.Exceptions;
using InfraConnect.Domain.Validators;

namespace InfraConnect.Domain.Entities.Users
{
    public class UserProfile : Base
    {
        public string FullName { get; private set; } = string.Empty;
        public string CPF { get; private set; } = string.Empty;
        public string? RG { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string? Phone { get; private set; }

        public Address Address { get; private set; } = default!;
        public Department DepartmentEnum { get; private set; }
        public JobTitle JobTitleEnum { get; private set; }
        public DateTime? AdmissionDate { get; private set; }
        public string? ProfileImageUrl { get; private set; }

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

            cpf = CPFValidator.EnsureValid(cpf);

            if (!string.IsNullOrWhiteSpace(rg))
            {
                rg = rg.Trim();
                if (rg.Length < 5 || rg.Length > 20)
                    throw new UserException("RG deve ter entre 5 e 20 caracteres.");
                RG = rg;
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                PhoneValidator.Validate(phone);
                Phone = phone.Trim();
            }

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

                BirthDate = birth;
            }

            if (admissionDate.HasValue && admissionDate.Value.Date > DateTime.UtcNow.Date)
                throw new UserException("A data de admissão não pode estar no futuro.");

            FullName = fullName.Trim();
            CPF = cpf;
            AdmissionDate = admissionDate;
            ProfileImageUrl = profileImageUrl?.Trim();
            DepartmentEnum = department;
            JobTitleEnum = jobTitle;
            Address = address ?? throw new UserException("Endereço é obrigatório.");
        }
    }
}
