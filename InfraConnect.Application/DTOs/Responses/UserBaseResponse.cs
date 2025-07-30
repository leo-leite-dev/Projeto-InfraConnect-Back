namespace InfraConnect.Application.DTOs.Responses
{
    public abstract class UserBaseResponse
    {
        public string Email { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public bool IsActive { get; init; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    }
}