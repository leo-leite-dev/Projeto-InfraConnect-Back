using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameNewsBoard.Application.IServices.Auth;
using InfraConnect.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InfraConnect.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateToken(UserBase user, TimeSpan? expiration = null)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            switch (user)
            {
                case User internalUser:
                    claims.Add(new Claim(ClaimTypes.Name, internalUser.Username ?? ""));
                    claims.Add(new Claim(ClaimTypes.Role, internalUser.Role.ToString()));
                    break;

                case ExternalAgent externalAgent:
                    claims.Add(new Claim(ClaimTypes.Name, externalAgent.Username ?? externalAgent.FullName));
                    claims.Add(new Claim(ClaimTypes.Role, externalAgent.ExternalRole.ToString()));
                    claims.Add(new Claim("company", externalAgent.Company));
                    break;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(expiration ?? TimeSpan.FromHours(1)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            try
            {
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

                return tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[TokenService] Falha ao validar token: " + ex.Message);
                return null;
            }
        }
    }
}
