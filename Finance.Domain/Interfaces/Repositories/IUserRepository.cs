using Finance.Domain.Models.Entities;

namespace Finance.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}
