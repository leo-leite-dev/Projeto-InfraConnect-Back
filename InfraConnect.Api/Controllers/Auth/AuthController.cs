using Microsoft.AspNetCore.Mvc;
using InfraConnect.Application.IServices.Auth;
using InfraConnect.Application.IServices.IAuths;
using InfraConnect.Application.DTOs.Requests.Auth;
using GameNewsBoard.Api.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace InfraConnect.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICookieService _cookieService;
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            ICookieService cookieService,
            IAuthService authService,
            ILogger<AuthController> logger)
        {
            _cookieService = cookieService ?? throw new ArgumentNullException(nameof(cookieService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("register-internal")]
        [Authorize]
        public async Task<IActionResult> RegisterInternal([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return ApiResponseHelper.CreateError("Erro de validação", "Dados inválidos.", 400);

            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(roleClaim))
                return ApiResponseHelper.CreateError("Não autenticado", "Token inválido", 401);

            try
            {
                var result = await _authService.RegisterUserAsync(request, roleClaim);

                if (!result.IsSuccess)
                {
                    if (result.Error is "Nome de usuário já está em uso." or "E-mail já está em uso.")
                        return ApiResponseHelper.CreateError("Conflito", result.Error, 409);

                    return ApiResponseHelper.CreateError("Falha no registro", result.Error, 400);
                }

                return Ok(ApiResponseHelper.CreateSuccess(result.Value, "Usuário registrado com sucesso"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao registrar usuário.");
                return ApiResponseHelper.CreateError("Erro no servidor", "Tente novamente mais tarde", 500);
            }
        }

        [HttpPost("register-external")]
        [Authorize]
        public async Task<IActionResult> RegisterExternal([FromBody] RegisterExternalAgentRequest request)
        {
            if (!ModelState.IsValid)
                return ApiResponseHelper.CreateError("Erro de validação", "Dados inválidos para criação do agente externo.", 400);

            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(roleClaim))
                return Unauthorized(ApiResponseHelper.CreateError("Não autenticado", "Token inválido", 401));

            try
            {
                var result = await _authService.RegisterExternalAgentAsync(request, roleClaim);

                if (!result.IsSuccess)
                {
                    if (result.Error is "Nome de usuário já está em uso." or "E-mail já está em uso.")
                        return ApiResponseHelper.CreateError("Conflito", result.Error, 409);

                    return ApiResponseHelper.CreateError("Falha no registro", result.Error, 400);

                }

                return Ok(ApiResponseHelper.CreateSuccess(result.Value, "Agente externo registrado com sucesso"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao registrar agente externo.");
                return ApiResponseHelper.CreateError("Erro no servidor", "Tente novamente mais tarde", 500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return ApiResponseHelper.CreateError("Erro de validação", "Dados de login inválidos.", 400);

            try
            {
                var result = await _authService.AuthenticateAsync(request);

                if (!result.IsSuccess || string.IsNullOrWhiteSpace(result.Value))
                    return ApiResponseHelper.CreateError("Falha na autenticação", result.Error ?? "Usuário ou senha incorretos.", 401);

                var token = result.Value;
                _cookieService.SetJwtCookie(Response, token, TimeSpan.FromHours(1));

                return Ok(ApiResponseHelper.CreateSuccess("Login realizado com sucesso", token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao fazer login.");
                return ApiResponseHelper.CreateError("Erro no servidor", "Ocorreu um erro ao processar o login. Tente novamente mais tarde.", 500);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                _cookieService.ClearJwtCookie(Response);
                return Ok(ApiResponseHelper.CreateSuccess("Logout realizado com sucesso"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao fazer logout.");
                return ApiResponseHelper.CreateError("Erro no servidor", "Erro ao encerrar a sessão. Tente novamente mais tarde.", 500);
            }
        }
    }
}