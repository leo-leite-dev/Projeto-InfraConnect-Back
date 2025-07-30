using InfraConnect.Domain.Enums;

namespace InfraConnect.Application.DTOs.Responses
{
    public record UserProfileResponse
    {
        public string FullName { get; init; } = string.Empty;
        public Department DepartmentEnum { get; private set; }
        public JobTitle JobTitleEnum { get; private set; }
    }
}