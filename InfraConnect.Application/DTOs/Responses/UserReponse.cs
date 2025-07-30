using InfraConnect.Domain.Enums;

namespace InfraConnect.Application.DTOs.Responses
{
    public class UserResponse : UserBaseResponse
    {
        public UserRole Role { get; set; }
        public UserProfileResponse Profile { get; set; } = null!;
    }
}
