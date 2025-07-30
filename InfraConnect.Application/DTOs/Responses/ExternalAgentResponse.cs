using InfraConnect.Domain.Enums;

namespace InfraConnect.Application.DTOs.Responses
{
    public class ExternalAgentResponse : UserBaseResponse
    {
        public string FullName { get; private set; } = string.Empty;
        public UserExternalRole Role { get; set; }
    }
}