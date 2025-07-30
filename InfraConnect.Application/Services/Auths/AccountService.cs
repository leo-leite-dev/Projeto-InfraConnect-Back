using InfraConnect.Application.Commons;
using InfraConnect.Application.DTOs.Requests.Auth;
using InfraConnect.Application.IRepositories;
using InfraConnect.Application.IServices.Auths;
using InfraConnect.Domain.Commons;
using InfraConnect.Domain.Exceptions;

namespace InfraConnect.Application.Services.Auth
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<AccountService> _logger;

        public AccountService(IUserRepository userRepository, IAppLogger<AccountService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<string>> ChangeUsernameAsync(ChangeUsernameRequest request)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null)
                    return Result<string>.Failure("Usuário não encontrado.");

                if (await _userRepository.ExistsByUsernameAsync(request.NewUsername))
                    return Result<string>.Failure("Nome de usuário já está em uso.");

                user.SetUsername(request.NewUsername, username => _userRepository.ExistsByUsernameAsync(username).Result);

                await _userRepository.UpdateAsync(user);

                return Result<string>.Success("Nome de usuário alterado com sucesso.");
            }
            catch (UserException ex)
            {
                return Result<string>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao alterar nome de usuário.");
                return Result<string>.Failure("Erro interno ao alterar nome de usuário.");
            }
        }
    }
}