﻿using Kanban.Application.Common.Models;
using Kanban.Domain.Entities;

namespace Kanban.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseData<List<User>>> GetUsers();
        Task<ResponseData<User>> GetUserById(int id);
        Task<Response> InsertUser(User user);
        Task<Response> UpdateUser(User user);
        Task<Response> DeleteUser(int id);
    }
}
