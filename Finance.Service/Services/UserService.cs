using Finance.Domain.Dtos;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Interfaces.Services;
using Finance.Domain.Models.Entities;

namespace Finance.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public UserService(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<bool> AddUserAsync(UserRequest request)
        {
            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            return await _userRepository.AddUserAsync(new User(request.Name, request.Email, passwordSalt, passwordHash));
        }

        public async Task<Result<User>> GetUserByEmailAsync(string email)
        {
            var result = await _userRepository.GetUserByEmailAsync(email);

            return result is null ? Result.Failure<User>("No user found.") :
                Result.Ok(result);
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await GetUserByEmailAsync(request.Email);

            if (!user.Success || user.Value is null)
            {
                return Result.Failure<LoginResponse>(user.Error);
            }

            var passwordValid = _authService.VerifyPasswordHash(request.Password, user.Value.PasswordHash, user.Value.PasswordSalt);

            if (!passwordValid)
            {
                return Result.Failure<LoginResponse>("Email and password does not match.");
            }

            var token = _authService.CreateJwtToken(user.Value.Email);

            return Result.Ok(new LoginResponse("Bearer", token));
        }
    }
}
