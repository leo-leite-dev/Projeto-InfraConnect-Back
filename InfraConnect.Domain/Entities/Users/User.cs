using InfraConnect.Domain.Entities.Commons;
using InfraConnect.Domain.Enums;

namespace InfraConnect.Domain.Entities.Users
{
    public class User : Base
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string? RG { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }

        public Address Address { get; set; } = new Address();

        public Department DepartmentEnum { get; set; }
        public JobTitle JobTitleEnum { get; set; }
        public DateTime? AdmissionDate { get; set; }

        public string? ProfileImageUrl { get; set; }

        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}