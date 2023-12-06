using Kanban.Domain.Entities;

namespace Kanban.Infraestructure.Kanban.Interfaces
{
  public interface IUserRepository
  {
    Task<List<User>> GetUsers();
    Task<User> GetUserById(int id);
    Task<User> GetByUserName(string username);
    Task InsertUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int id);
  }
}
