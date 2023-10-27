using Kanban.Application.DTOs.Users.Request;
using Kanban.Application.DTOs.Users.Response;
using Kanban.Application.Interfaces.Services;
using Kanban.Application.Models;
using Kanban.Application.Validators.Users;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;

namespace Kanban.Application.Services
{
    public class UserService : BaseService<UserValidators>, IUserService
  {

    public UserService(IUnitOfWork unitOfWork, UserValidators validator) : base(unitOfWork, validator)
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

    public async Task<ResponseData<CredentialsDTO>> Login(LoginDTO login)
    {
      return new()
      {
        IsSuccess = false
      };
    }
  }
}
