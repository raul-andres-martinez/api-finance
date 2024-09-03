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

        private async Task<CustomActionResult> CheckDuplicateUserAsync(string email)
        {
            var duplicate = await _context.Users
                .Where(u => u.Email == email).FirstOrDefaultAsync();

            if (duplicate is not null)
            {
                return UserError.EmailAlreadyInUse;
            }

            return CustomActionResult.NoContent();
        }

        public async Task<CustomActionResult<User>> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
            //var result = await _userRepository.GetUserByEmailAsync(email);

            //return result is null ? 
            //    Result.Failure<User>("No user found.", ErrorCode.USER_NOT_FOUND):
            //    Result.Ok(result);
        }

        public async Task<CustomActionResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();

            //var user = await GetUserByEmailAsync(request.Email);

            //if (!user.Success || user.Value is null)
            //{
            //    return Result.Failure<LoginResponse>(ErrorMessages.Auth.InvalidLogin, ErrorCode.RESOURCE_NOT_FOUND);
            //}

            //var passwordValid = PasswordHasher.VerifyPasswordHash(request.Password, user.Value.PasswordHash, user.Value.PasswordSalt);

            //if (!passwordValid)
            //{
            //    return Result.Failure<LoginResponse>(ErrorMessages.Auth.InvalidLogin, ErrorCode.RESOURCE_NOT_FOUND);
            //}

            //var token = JwtUtils.CreateJwtToken(user.Value.Email);

            //return Result.Ok(new LoginResponse("Bearer", token));
        }
    }
}