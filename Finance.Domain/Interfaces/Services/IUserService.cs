using Finance.Domain.Dtos;

namespace Finance.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(UserRequest request);
    }
}
