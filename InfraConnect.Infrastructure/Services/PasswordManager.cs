using InfraConnect.Application.IServices;

public class PasswordManager : IPasswordManager
{
    public string Hash(string plainPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }
}