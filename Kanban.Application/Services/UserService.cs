using Kanban.Application.DTOs.Users.Request;
using Kanban.Application.DTOs.Users.Response;
using Kanban.Application.Interfaces.Services;
using Kanban.Application.Models;
using Kanban.Application.Validators.Users;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.UnitsOfWork;
using System.Security.Principal;
using BC = BCrypt.Net.BCrypt;

namespace Kanban.Application.Services
{
  public class UserService : BaseService<UserValidators>, IUserService
  {

    public UserService(IUnitOfWork unitOfWork, UserValidators validator) : base(unitOfWork, validator)
    {
    }

    public Task<ResponseData<List<User>>> GetUsers()
    {
      throw new NotImplementedException();
    }

    public Task<ResponseData<User>> GetUserById(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<Response> InsertUser(User user)
    {
      var response = new Response();
      user.Password = BC.HashPassword(user.Password);
      try
      {
        await _unitOfWork.UserRepository.InsertUser(user);
        _unitOfWork.Commit();
        return response;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
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
      var response = new ResponseData<CredentialsDTO>();
      var validationResult = await _validator.Login(login);
      if (!validationResult.IsValid)
      {
        response.NotValid(validationResult.Errors);
        return response;
      }

      var user = await _unitOfWork.UserRepository.GetByUserName(login.UserName);



      return new();
    }
  }
}
