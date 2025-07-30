using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InfraConnect.Application.IServices;
using InfraConnect.Application.DTOs.Requests.Auth;
using GameNewsBoard.Api.Helpers;

namespace InfraConnect.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
        }

        [HttpPost("change")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
                return ApiResponseHelper.CreateError("Erro de validação", "Dados inválidos.", 400);

            var result = await _passwordService.ChangePasswordAsync(request);

            if (!result.IsSuccess)
                return ApiResponseHelper.CreateError("Falha na troca de senha", result.Error, 400);

            return Ok(ApiResponseHelper.CreateSuccess(result.Value, "Senha alterada com sucesso"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (request.NewPassword != request.ConfirmPassword)
            {
                return ApiResponseHelper.CreateError(
                    message: "Falha na redefinição",
                    detail: "As senhas não coincidem.",
                    statusCode: 409
                );
            }

            var result = await _passwordService.ResetPasswordWithTokenAsync(request);

            if (!result.IsSuccess)
            {
                return ApiResponseHelper.CreateError(
                    message: "Falha na redefinição",
                    detail: result.Error,
                    statusCode: 400
                );
            }

            return Ok(ApiResponseHelper.CreateSuccess(result.Value, "Senha redefinida com sucesso"));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return ApiResponseHelper.CreateError("Erro de validação", "E-mail inválido.", 400);

            var result = await _passwordService.GeneratePasswordResetTokenAsync(request.Email);

            if (!result.IsSuccess)
                return ApiResponseHelper.CreateError("Falha ao enviar e-mail", result.Error, 400);

            return Ok(ApiResponseHelper.CreateSuccess("E-mail de recuperação enviado com sucesso"));
        }
    }
}