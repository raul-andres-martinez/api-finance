using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;

namespace Finance.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<CustomActionResult<User>> GetUserByEmailAsync(string email);
        Task<CustomActionResult> AddUserAsync(User user);
    }
}
