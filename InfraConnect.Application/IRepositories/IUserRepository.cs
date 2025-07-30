using InfraConnect.Domain.Entities.Users;

namespace InfraConnect.Application.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> ExistsByUsernameAsync(string username);
        Task AddUserProfileAsync(UserProfile profile);
    }
}