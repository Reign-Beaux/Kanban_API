using Kanban.Application.Models;
using Kanban.Domain.Entities;

namespace Kanban.Application.Interfaces
{
  public interface IUserService
  {
    Task<ResponseData<List<User>>> GetAll();
    Task<ResponseData<User>> GetById(int id);
    Task<ResponseData<User>> GetByUsername(string username);
    Task<Response> InsertUser(User user);
    Task<Response> UpdateUser(User user);
    Task<Response> DeleteUser(int id);
  }
}
