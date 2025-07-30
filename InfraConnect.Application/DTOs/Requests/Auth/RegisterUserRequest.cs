namespace InfraConnect.Application.DTOs.Requests.Auth
{
    using InfraConnect.Domain.Enums;

    public class RegisterUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string? RG { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string? ProfileImageUrl { get; set; }

        public Department Department { get; set; }
        public JobTitle JobTitle { get; set; }

        public AddressRequest Address { get; set; } = new();

        public UserRole Role { get; set; } = UserRole.Trainee;

        public string? PasswordHash { get; set; }
    }
}