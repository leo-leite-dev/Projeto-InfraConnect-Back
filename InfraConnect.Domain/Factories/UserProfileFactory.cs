using InfraConnect.Domain.Entities.Users;
using InfraConnect.Domain.Enums;

namespace InfraConnect.Domain.Factories
{
    public static class UserProfileFactory
    {
        public static UserProfile Create(
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
            return new UserProfile(
                fullName: fullName,
                cpf: cpf,
                department: department,
                jobTitle: jobTitle,
                address: address,
                rg: rg,
                birthDate: birthDate,
                phone: phone,
                admissionDate: admissionDate,
                profileImageUrl: profileImageUrl
            );
        }
    }
}