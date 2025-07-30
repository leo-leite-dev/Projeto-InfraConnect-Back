namespace InfraConnect.Application.IServices
{
    public interface IPasswordManager
    {
        string Hash(string plainPassword);
    }
}