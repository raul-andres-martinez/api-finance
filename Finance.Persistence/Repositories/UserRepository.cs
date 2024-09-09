using Finance.Domain.Errors;
using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Models.Entities;
using Finance.Domain.Utils.Result;
using Finance.Persistence.Context;
using System.Data.Entity;

namespace Finance.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinanceContext _context;

        public UserRepository(FinanceContext context)
        {
            _context = context;
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

        public async Task<CustomActionResult> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            var changes = await _context.SaveChangesAsync();

            if (changes is not 1)
            {
                return UserError.FailedToCreate;
            }

            return CustomActionResult.Created();
        }
    }
}
