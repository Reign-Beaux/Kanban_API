using Kanban.Application.DTOs.Users.Request;
using Kanban.Application.DTOs.Users.Response;
using Kanban.Application.Models;
using Kanban.Domain.Entities;

namespace Kanban.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResponseData<List<User>>> GetUsers();
        Task<ResponseData<User>> GetUserById(int id);
        Task<Response> InsertUser(User user);
        Task<Response> UpdateUser(User user);
        Task<Response> DeleteUser(int id);
        Task<ResponseData<CredentialsDTO>> Login(LoginDTO login);
    }
}
