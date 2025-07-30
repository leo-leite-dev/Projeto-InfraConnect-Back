namespace InfraConnect.Application.DTOs.Requests.Auth
{
    public class ChangeUsernameRequest
    {
        public Guid UserId { get; set; }
        public string NewUsername { get; set; } = string.Empty;
    }
}