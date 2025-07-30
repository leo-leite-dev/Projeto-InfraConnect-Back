using AutoMapper;
using GameNewsBoard.Application.IServices.Auth;
using InfraConnect.Application.DTOs.Requests.Auth;
using InfraConnect.Application.DTOs.Responses;
using InfraConnect.Application.IRepositories;
using InfraConnect.Application.IServices;
using InfraConnect.Application.IServices.IAuths;
using InfraConnect.Application.Validators;
using InfraConnect.Domain.Commons;
using InfraConnect.Domain.Entities.Users;
using InfraConnect.Domain.Enums;
using InfraConnect.Domain.Exceptions;
using InfraConnect.Domain.Factories;
using InfraConnect.Domain.Validators;
using Microsoft.Extensions.Logging;

namespace InfraConnect.Application.Services.Auths
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IExternalAgentRepository _externalAgentRepository;
        private readonly IUserBaseRepository _userBaseRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IUserRepository userRepository,
            IExternalAgentRepository externalAgentRepository,
            IUserBaseRepository userBaseRepository,
            IPasswordManager passwordManager,
            ITokenService tokenService,
            IMapper mapper,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _externalAgentRepository = externalAgentRepository ?? throw new ArgumentNullException(nameof(externalAgentRepository));
            _userBaseRepository = userBaseRepository ?? throw new ArgumentNullException(nameof(userBaseRepository));
            _passwordManager = passwordManager ?? throw new ArgumentNullException(nameof(passwordManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<UserResponse>> RegisterUserAsync(RegisterUserRequest request, string currentUserRole)
        {
            if (!string.Equals(currentUserRole, UserRole.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                return Result<UserResponse>.Failure("Apenas administradores podem registrar novos usuários.");

            if (!DepartmentJobValidator.IsValidJobTitle(request.Department, request.JobTitle))
                return Result<UserResponse>.Failure("O cargo selecionado não é válido para o departamento informado.");

            try
            {
                if (await _userBaseRepository.ExistsByUsernameAsync(request.Username))
                    return Result<UserResponse>.Failure("Nome de usuário já está em uso.");

                if (await _userBaseRepository.ExistsByEmailAsync(request.Email))
                    return Result<UserResponse>.Failure("E-mail já está em uso.");

                request.Username = await GenerateUsernameIfEmptyAsync(request.Username);

                var profile = _mapper.Map<UserProfile>(request);

                var newUser = UserFactory.Create(
                    email: request.Email,
                    passwordHash: string.Empty,
                    role: request.Role,
                    profile: profile,
                    username: request.Username
                );

                newUser.SetPassword(request.Password, _passwordManager.Hash);

                await _userRepository.AddAsync(newUser);

                var response = _mapper.Map<UserResponse>(newUser);
                return Result<UserResponse>.Success(response);
            }
            catch (UserException ex)
            {
                _logger.LogWarning(ex, "Erro de validação de usuário.");
                return Result<UserResponse>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao registrar usuário.");
                return Result<UserResponse>.Failure("Erro interno ao registrar usuário.");
            }
        }

        public async Task<Result<ExternalAgentResponse>> RegisterExternalAgentAsync(
            RegisterExternalAgentRequest request,
            string currentUserRole)
        {
            if (!string.Equals(currentUserRole, UserRole.Admin.ToString(), StringComparison.OrdinalIgnoreCase))
                return Result<ExternalAgentResponse>.Failure("Apenas administradores podem registrar novos agentes externos.");

            try
            {
                if (await _userBaseRepository.ExistsByUsernameAsync(request.Username))
                    return Result<ExternalAgentResponse>.Failure("Nome de usuário já está em uso.");

                if (await _userBaseRepository.ExistsByEmailAsync(request.Email))
                    return Result<ExternalAgentResponse>.Failure("E-mail já está em uso.");

                request.Username = await GenerateUsernameIfEmptyAsync(request.Username);

                var agent = ExternalAgentFactory.Create(
                    fullName: request.FullName,
                    email: request.Email,
                    passwordHash: string.Empty, 
                    company: request.Company,
                    jobTitle: request.JobTitle,
                    phone: request.Phone,
                    accessExpiresAt: request.AccessExpiresAt,
                    role: request.Role,
                    username: request.Username
                );

                agent.SetPassword(request.Password, _passwordManager.Hash);

                await _externalAgentRepository.AddAsync(agent);

                var response = _mapper.Map<ExternalAgentResponse>(agent);
                return Result<ExternalAgentResponse>.Success(response);
            }
            catch (UserException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao registrar agente externo.");
                return Result<ExternalAgentResponse>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao registrar agente externo.");
                return Result<ExternalAgentResponse>.Failure("Erro interno ao registrar o agente externo.");
            }
        }

        public async Task<Result<string>> AuthenticateAsync(LoginRequest request)
        {
            try
            {
                User? user;

                if (IsEmail(request.Identifier))
                    user = await _userRepository.GetByEmailAsync(request.Identifier);
                else
                    user = await _userRepository.GetByUsernameAsync(request.Identifier);

                if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                    return Result<string>.Failure("Usuário ou senha inválidos.");

                var token = _tokenService.GenerateToken(user);

                return Result<string>.Success(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao autenticar usuário com identificador {Identifier}", request.Identifier);
                return Result<string>.Failure("Erro interno ao autenticar usuário.");
            }
        }

        private async Task<string> GenerateUsernameIfEmptyAsync(string? username)
        {
            if (!string.IsNullOrWhiteSpace(username))
                return username.Trim();

            int i = 1;
            string generated;
            do
            {
                generated = $"usuario{i++}";
            } while (await _userRepository.ExistsByUsernameAsync(generated));

            return generated;
        }

        private bool IsEmail(string identifier)
        {
            return identifier.Contains('@');
        }
    }
}