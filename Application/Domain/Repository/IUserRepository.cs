﻿using Application.Domain.Entity;

namespace Application.Domain.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User data);
        Task<User> UpdateUser(User data);
        Task<User> GetUserById(string id);
    }
}