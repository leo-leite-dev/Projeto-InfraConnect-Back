using InfraConnect.Application.IRepositories;
using InfraConnect.Domain.Entities.Users;
using InfraConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraConnect.Infrastructure.Repositories
{
    public class UserBaseRepository : IUserBaseRepository
    {
        private readonly AppDbContext _context;

        public UserBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Set<User>().AnyAsync(u => u.Email == email)
                || await _context.Set<ExternalAgent>().AnyAsync(a => a.Email == email);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.Set<User>().AnyAsync(u => u.Username == username)
                || await _context.Set<ExternalAgent>().AnyAsync(a => a.Username == username);
        }
    }
}