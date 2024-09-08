using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Errors;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils;
using Finance.Domain.Utils.Result;
using Finance.Persistence.Context;
using System.Data.Entity;
using static Finance.Domain.Constants.Constant;

namespace Finance.Service.Services
{
    public class UserService : IUserService
    {
        private readonly FinanceContext _context;

        public UserService(FinanceContext context)
        {
            _context = context;
        }

        public async Task<CustomActionResult> AddUserAsync(UserRequest request)
        {
            var duplicate = await CheckDuplicateUserAsync(request.Email);

            if (!duplicate.Success)
            {
                return duplicate.Error;
            }

            PasswordHasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            await _context.Users.AddAsync(request.ToEntity(passwordHash, passwordSalt));
            var changes = await _context.SaveChangesAsync();
            
            if (changes is not 1)
            {
                return UserError.FailedToCreate;
            }

            return CustomActionResult.Created();
        }

        public async Task<CustomActionResult<User>> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .Where(u => u.Email == email).FirstOrDefaultAsync();
            
            if (user is null)
            {
                return UserError.NotFound;
            }

            return user;
        }

        public async Task<CustomActionResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await GetUserByEmailAsync(request.Email);

            if (!user.Success)
            {
                return UserError.InvalidLogin;
            }

            var passwordValid = PasswordHasher.VerifyPasswordHash(request.Password, user.Value.PasswordHash, user.Value.PasswordSalt);

            if (!passwordValid)
            {
                return UserError.InvalidLogin;
            }

            var token = JwtUtils.CreateJwtToken(user.Value.Email);

            return new LoginResponse(TokenType.Bearer, token);
        }

        private async Task<CustomActionResult> CheckDuplicateUserAsync(string email)
        {
            var duplicate = await GetUserByEmailAsync(email);

            if (duplicate is not null)
            {
                return UserError.EmailAlreadyInUse;
            }

            return CustomActionResult.NoContent();
        }
    }
}