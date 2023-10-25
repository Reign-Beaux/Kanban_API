using Kanban.Application.Interfaces;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class UserService : BaseService, IUserService
  {
    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
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
