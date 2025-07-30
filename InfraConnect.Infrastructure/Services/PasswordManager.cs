using InfraConnect.Application.IServices;

namespace InfraConnect.Infrastructure.Services
{
    public class PasswordManager : IPasswordManager
    {
        public string Hash(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }

        public bool Verify(string rawPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(rawPassword, passwordHash);
        }
    }
}