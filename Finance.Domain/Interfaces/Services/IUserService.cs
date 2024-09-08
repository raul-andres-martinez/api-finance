using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;

namespace Finance.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<CustomActionResult> AddUserAsync(UserRequest request);
        Task<CustomActionResult<User>> GetUserByEmailAsync(string email);
        Task<CustomActionResult<LoginResponse>> LoginAsync(LoginRequest request);
    }
}