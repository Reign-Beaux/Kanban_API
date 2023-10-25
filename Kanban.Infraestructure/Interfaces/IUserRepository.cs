using Kanban.Domain.Entities;

namespace Kanban.Infraestructure.Interfaces
{
  public interface IUserRepository
  {
    Task<List<User>> GetAll();
    Task<User> GetById(int id);
    Task<User> GetByUsername(string username);
    Task InsertUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int id);
  }
}
