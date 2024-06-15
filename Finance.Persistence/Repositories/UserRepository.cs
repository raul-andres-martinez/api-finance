﻿using Finance.Domain.Interfaces.Repositories;
using Finance.Domain.Models.Entities;
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

        public async Task<bool> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
