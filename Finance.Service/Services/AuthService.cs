using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Configs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Finance.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly EncryptionConfigs _encryptionConfigs;
        private readonly JwtConfigs _jwtConfigs;

        public AuthService(EncryptionConfigs encryptionConfigs, JwtConfigs jwtConfigs)
        {
            _encryptionConfigs = encryptionConfigs;
            _jwtConfigs = jwtConfigs;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateJwtToken(string email)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = _jwtConfigs.JwtKey ?? throw new InvalidOperationException();
            var issuer = _jwtConfigs.Issuer ?? throw new InvalidOperationException();

            var privateKey = Encoding.UTF8.GetBytes(key);

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
                Issuer = issuer,
                Subject = claim
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
