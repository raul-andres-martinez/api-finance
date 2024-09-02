using Finance.Domain.Models.Configs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finance.Domain.Utils
{
    public static class JwtUtils
    { 
        public static string CreateJwtToken(string email)
        {
            var configuration = GetJwtConfiguration();

            var handler = new JwtSecurityTokenHandler();
            var privateKey = Encoding.UTF8.GetBytes(configuration.JwtKey!);

            var credentials = new SigningCredentials(
                        new SymmetricSecurityKey(privateKey),
                        SecurityAlgorithms.HmacSha256);

            var claim = new ClaimsIdentity();
            claim.AddClaim(new Claim(ClaimTypes.Email, email));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(12),
                Issuer = configuration.Issuer,
                Subject = claim
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static JwtConfiguration GetJwtConfiguration()
        {
            var jwtConfiguration = new JwtConfiguration();

            _ = jwtConfiguration.JwtKey ?? throw new InvalidOperationException(nameof(jwtConfiguration.JwtKey));
            _ = jwtConfiguration.Issuer ?? throw new InvalidOperationException(nameof(jwtConfiguration.Issuer));

            return jwtConfiguration;
        }
    }
}