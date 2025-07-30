namespace InfraConnect.Application.IRepositories
{
    public interface IUserBaseRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
    }
}