using InfraConnect.Application.DTOs.Requests.Auth;
using InfraConnect.Domain.Commons;

namespace InfraConnect.Application.IServices
{
    public interface IPasswordService
    {
        Task<Result<string>> ChangePasswordAsync(ChangePasswordRequest request);
        Task<Result<string>> GeneratePasswordResetTokenAsync(string email);
        Task<Result<string>> ResetPasswordWithTokenAsync(ResetPasswordRequest request);
    }
}