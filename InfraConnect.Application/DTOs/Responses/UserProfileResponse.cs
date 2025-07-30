namespace InfraConnect.Application.DTOs.Responses
{
    public record UserProfileResponse
    {
        public string FullName { get; init; } = string.Empty;
        public bool IsActive { get; init; }
    }
}