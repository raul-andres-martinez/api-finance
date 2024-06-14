using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Configs;
using System.Security.Cryptography;
using System.Text;

namespace Finance.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly EncryptionConfigs _encryptionConfigs;

        public AuthService(EncryptionConfigs encryptionConfigs)
        {
            _encryptionConfigs = encryptionConfigs;
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
    }
}
