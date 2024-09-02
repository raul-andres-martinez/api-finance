using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils;
using Finance.Persistence.Context;
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

        public async Task<bool> AddUserAsync(UserRequest request)
        {
            PasswordHasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            return await _userRepository.AddUserAsync(new User(request.Name, request.Email, passwordSalt, passwordHash));
        }

        public async Task<Result<User>> GetUserByEmailAsync(string email)
        {
            var result = await _userRepository.GetUserByEmailAsync(email);

            return result is null ? 
                Result.Failure<User>("No user found.", ErrorCode.USER_NOT_FOUND):
                Result.Ok(result);
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await GetUserByEmailAsync(request.Email);

            if (!user.Success || user.Value is null)
            {
                return Result.Failure<LoginResponse>(ErrorMessages.Auth.InvalidLogin, ErrorCode.RESOURCE_NOT_FOUND);
            }

            var passwordValid = PasswordHasher.VerifyPasswordHash(request.Password, user.Value.PasswordHash, user.Value.PasswordSalt);

            if (!passwordValid)
            {
                return Result.Failure<LoginResponse>(ErrorMessages.Auth.InvalidLogin, ErrorCode.RESOURCE_NOT_FOUND);
            }

            var token = JwtUtils.CreateJwtToken(user.Value.Email);

            return Result.Ok(new LoginResponse("Bearer", token));
        }
    }
}
