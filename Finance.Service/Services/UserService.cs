using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Errors;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Configs;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils;
using Finance.Domain.Utils.Result;
using static Finance.Domain.Constants.Constant;

namespace Finance.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppConfig _appConfig;

        public UserService(IUserRepository userRepository, AppConfig appConfig)
        {
            _userRepository = userRepository;
            _appConfig = appConfig;
        }

        public async Task<CustomActionResult> AddUserAsync(UserRequest request)
        {
            var duplicate = await CheckDuplicateUserAsync(request.Email);

            if (!duplicate.Success)
            {
                return duplicate.GetError();
            }

            PasswordHasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = request;
            user.SetHashSalt(passwordHash, passwordSalt);

            return await _userRepository.AddUserAsync(user);
        }

        public async Task<CustomActionResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var userResult = await _userRepository.GetUserByEmailAsync(request.Email);

            if (!userResult.Success)
            {
                return UserError.InvalidLogin;
            }

            var user = userResult.GetValue();

            var passwordValid = PasswordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

            if (!passwordValid)
            {
                return UserError.InvalidLogin;
            }

            var token = JwtUtils.CreateJwtToken(user.Email, _appConfig.JwtConfigs.JwtKey, _appConfig.JwtConfigs.Issuer);

            return new LoginResponse(TokenType.Bearer, token);
        }

        private async Task<CustomActionResult> CheckDuplicateUserAsync(string email)
        {
            var duplicate = await _userRepository.GetUserByEmailAsync(email);

            if (duplicate.Success)
            {
                return UserError.EmailAlreadyInUse;
            }

            return CustomActionResult.NoContent();
        }
    }
}