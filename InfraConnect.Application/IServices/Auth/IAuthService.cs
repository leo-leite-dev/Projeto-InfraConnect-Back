using InfraConnect.Application.DTOs.Requests.Auth;
using InfraConnect.Application.DTOs.Responses;
using InfraConnect.Domain.Commons;

namespace InfraConnect.Application.IServices.IAuths
{
    public interface IAuthService
    {
        Task<Result<string>> AuthenticateAsync(LoginRequest request);
        Task<Result<UserResponse>> RegisterUserAsync(RegisterUserRequest request, string currentUserRole);
        Task<Result<ExternalAgentResponse>> RegisterExternalAgentAsync(RegisterExternalAgentRequest request, string role);
    }
}