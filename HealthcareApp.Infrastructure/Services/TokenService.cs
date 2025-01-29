using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using HealthcareApp.Application.Interfaces;
using HealthcareApp.Infrastructure.Configuration;
using HealthcareApp.Domain.Entities;   
using Microsoft.IdentityModel.Tokens;

namespace HealthcareApp.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

      public TokenService(IOptions<JwtSettings> jwtSettings)
{
    if (jwtSettings == null || jwtSettings.Value == null)
    {
        throw new ArgumentNullException(nameof(jwtSettings), "JwtSettings is null.");
    }

    _jwtSettings = jwtSettings.Value;

    // Validate JwtSettings
    if (string.IsNullOrEmpty(_jwtSettings.Secret) ||
        string.IsNullOrEmpty(_jwtSettings.Issuer) ||
        string.IsNullOrEmpty(_jwtSettings.Audience))
    {
        throw new InvalidOperationException("JWT settings are not properly configured.");
    }

    // Ensure the secret key is long enough
    var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
    if (keyBytes.Length < 32) // 32 bytes = 256 bits
    {
        throw new InvalidOperationException("JWT secret key must be at least 32 bytes (256 bits).");
    }
}

        public string GenerateToken(AppUser user)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _jwtSettings.Issuer,
                    _jwtSettings.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging library of your choice)
                throw new InvalidOperationException("Error generating token.", ex);
            }
        }
    }
}
