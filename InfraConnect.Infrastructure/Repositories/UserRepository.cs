using InfraConnect.Application.IRepositories;
using InfraConnect.Domain.Entities.Users;
using InfraConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraConnect.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.Set<User>().AnyAsync(u => u.Username == username);
        }

        public async Task AddUserProfileAsync(UserProfile profile)
        {
            await _context.Set<UserProfile>().AddAsync(profile);
            await _context.SaveChangesAsync();
        }
    }
}