using Kanban.Application.Interfaces;
using Kanban.Application.Models;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class UserService : BaseService, IUserService
  {
    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public Task<ResponseData<List<User>>> GetAll()
    {
      throw new NotImplementedException();
    }

    public Task<ResponseData<User>> GetById(int id)
    {
      throw new NotImplementedException();
    }

    public Task<ResponseData<User>> GetByUsername(string username)
    {
      throw new NotImplementedException();
    }

    public Task<Response> InsertUser(User user)
    {
      throw new NotImplementedException();
    }

    public Task<Response> UpdateUser(User user)
    {
      throw new NotImplementedException();
    }

    public Task<Response> DeleteUser(int id)
    {
      throw new NotImplementedException();
    }
  }
}
