
using System.Security.Claims;
using InfraConnect.Domain.Entities.Users;

namespace GameNewsBoard.Application.IServices.Auth
{
    public interface ITokenService
    {
        string GenerateToken(UserBase user, TimeSpan? expiration = null);
        ClaimsPrincipal? ValidateToken(string token);
    }
}