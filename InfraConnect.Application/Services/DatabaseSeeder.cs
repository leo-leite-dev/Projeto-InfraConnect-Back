using InfraConnect.Application.IRepositories;
using InfraConnect.Application.IServices;
using InfraConnect.Domain.Entities.Users;
using InfraConnect.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace InfraConnect.Infrastructure.Services
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserBaseRepository _userBaseRepository;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(
            IUserRepository userRepository,
            IUserBaseRepository userBaseRepository,
            ILogger<DatabaseSeeder> logger)
        {
            _userRepository = userRepository;
            _userBaseRepository = userBaseRepository;
            _logger = logger;
        }

        public async Task SeedAdminUserAsync()
        {
            const string adminEmail = "admin@sistema.com";

            var exists = await _userBaseRepository.ExistsByEmailAsync(adminEmail);
            if (exists)
            {
                _logger.LogInformation("Usuário administrador já existe. Nenhuma ação necessária.");
                return;
            }

            var profile = new UserProfile(
                fullName: "Administrador Master",
                cpf: "00000000000",
                department: Department.Regulation,
                jobTitle: JobTitle.Manager,
                address: new Address(
                    street: "Av. Central",
                    number: "100",
                    neighborhood: "Centro",
                    city: "Cidade",
                    state: "SP",
                    zipCode: "12345678"
                )
            );

            await _userRepository.AddUserProfileAsync(profile);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");

            var admin = new User(
                email: adminEmail,
                passwordHash: passwordHash,
                role: UserRole.Admin,
                profile: profile,
                username: "admin"
            );

            await _userRepository.AddAsync(admin);
            _logger.LogInformation("Usuário administrador criado com sucesso.");
        }
    }
}
