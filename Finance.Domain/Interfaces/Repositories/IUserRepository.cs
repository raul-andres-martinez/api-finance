using Finance.Domain.Models.Entities;

namespace Finance.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);
    }
}
