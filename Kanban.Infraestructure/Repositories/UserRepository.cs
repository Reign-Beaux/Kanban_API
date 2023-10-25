using Kanban.Domain.Entities;
using Kanban.Infraestructure.Interfaces;
using System.Data;

namespace Kanban.Infraestructure.Repositories
{
  public class UserRepository : BaseRepository, IUserRepository
  {
    public UserRepository(IDbTransaction dbTransaction) : base(dbTransaction)
    {
    }

    public Task<List<User>> GetAll()
    {
      throw new NotImplementedException();
    }

    public Task<User> GetById(int id)
    {
      throw new NotImplementedException();
    }

    public Task<User> GetByUsername(string username)
    {
      throw new NotImplementedException();
    }

    public Task InsertUser(User user)
    {
      throw new NotImplementedException();
    }

    public Task UpdateUser(User user)
    {
      throw new NotImplementedException();
    }

    public Task DeleteUser(int id)
    {
      throw new NotImplementedException();
    }
  }
}
