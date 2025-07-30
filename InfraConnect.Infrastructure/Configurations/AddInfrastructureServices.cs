using GameNewsBoard.Application.IServices.Auth;
using InfraConnect.Application.IRepositories;
using InfraConnect.Application.IServices;
using InfraConnect.Application.IServices.Auth;
using InfraConnect.Application.IServices.IAuths;
using InfraConnect.Application.Services.Auths;
using InfraConnect.Infrastructure.Data;
using InfraConnect.Infrastructure.Repositories;
using InfraConnect.Infrastructure.Services;
using InfraConnect.Infrastructure.Services.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfraConnect.Configurations.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExternalAgentRepository, ExternalAgentRepository>();
            services.AddScoped<IUserBaseRepository, UserBaseRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
            services.AddScoped<IPasswordManager, PasswordManager>();

            return services;
        }
    }
}