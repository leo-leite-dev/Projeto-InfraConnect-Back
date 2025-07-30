using InfraConnect.Application.IRepositories;
using InfraConnect.Domain.Entities.Users;
using InfraConnect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraConnect.Infrastructure.Repositories
{
    public class ExternalAgentRepository : GenericRepository<ExternalAgent>, IExternalAgentRepository
    {
        public ExternalAgentRepository(AppDbContext context) : base(context) { }
    }
}