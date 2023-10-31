using Kanban.Application.Interfaces.Services;
using Kanban.Application.Models;
using Kanban.Application.Statics;
using Kanban.Application.Utils;
using Kanban.Application.Validators.Users;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.UnitsOfWork;
using BC = BCrypt.Net.BCrypt;

namespace Kanban.Application.Services
{
  public class UserService : BaseService<UserValidator>, IUserService
  {

    public UserService(
      IUnitOfWork unitOfWork,
      UserValidator validator,
      ExceptionsLogger logger) : base(unitOfWork, validator, logger)
    {}

    public async Task<ResponseData<List<User>>> GetUsers()
    {
      var response = new ResponseData<List<User>>();
      try
      {
        response.Data = await _unitOfWork.UserRepository.GetUsers();
        response.Message =
          response.Data.Count > 0
          ? ReplyMessages.QUERY_SUCCESS
          : ReplyMessages.QUERY_EMPTY;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.QUERY_FAILED;
        _logger.SetException("Error al obtener usuarios: " + ex.Message);
      }

      return response;
    }

    public async Task<ResponseData<User>> GetUserById(int id)
    {
      var response = new ResponseData<User>();
      try
      {
        var user = await _unitOfWork.UserRepository.GetUserById(id);

        if (user is null)
        {
          response.Status = StatusResponse.NOT_FOUND;
          response.Message = ReplyMessages.RECORD_NOT_FOUND;
        }
        else
        {
          response.Data = user;
          response.Message = ReplyMessages.QUERY_SUCCESS;
        }
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.QUERY_FAILED;
        _logger.SetException("Error al obtener usuario por id: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> InsertUser(User user)
    {
      var response = new Response();
      var validationResult = await _validator.ExecuteValidateUser(user);
      if (!validationResult.IsValid)
      {
        response.NotValid(validationResult.Errors);
        return response;
      }

      user.Password = BC.HashPassword(user.Password);
      try
      {
        await _unitOfWork.UserRepository.InsertUser(user);
        _unitOfWork.Commit();
        response.Message = ReplyMessages.SAVE;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
        _logger.SetException("Error al insertar usuario: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> UpdateUser(User user)
    {
      var response = new Response();
      var currentUser = await _unitOfWork.UserRepository.GetUserById(user.Id);

      if (currentUser is null)
      {
        response.Status = StatusResponse.NOT_FOUND;
        response.Message = ReplyMessages.RECORD_NOT_FOUND;
        return response;
      }

      var validationResult = await _validator.ExecuteValidateUser(user);
      if (!validationResult.IsValid)
      {
        response.NotValid(validationResult.Errors);
        return response;
      }

      user.Password = BC.HashPassword(user.Password);
      try
      {
        await _unitOfWork.UserRepository.UpdateUser(user);
        _unitOfWork.Commit();
        response.Message = ReplyMessages.UPDATE;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
        _logger.SetException("Error al insertar usuario: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> DeleteUser(int id)
    {
      var response = new Response();
      var user = await _unitOfWork.UserRepository.GetUserById(id);

      if (user is null)
      {
        response.Status = StatusResponse.NOT_FOUND;
        response.Message = ReplyMessages.RECORD_NOT_FOUND;
        return response;
      }

      try
      {
        await _unitOfWork.UserRepository.DeleteUser(id);
        _unitOfWork.Commit();
        response.Message = ReplyMessages.DELETE;
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
        _logger.SetException("Error al eliminar usuario: " + ex.Message);
      }

      return response;
    }
  }
}
