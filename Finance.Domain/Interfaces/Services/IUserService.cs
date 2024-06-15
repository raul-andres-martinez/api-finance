﻿using Finance.Domain.Dtos;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Models.Entities;

namespace Finance.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(UserRequest request);
        Task<Result<User>> GetUserByEmailAsync(string email);
    }
}
