namespace Finance.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        string CreateJwtToken(string email);
    }
}
