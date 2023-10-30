using Kanban.Application.DTOs.Users.Request;
using Kanban.Application.DTOs.Users.Response;
using Kanban.Application.Interfaces.Services;
using Kanban.Application.Models;
using Kanban.Application.Statics;
using Kanban.Application.Utils;
using Kanban.Application.Validators.Users;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.UnitsOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace Kanban.Application.Services
{
  public class UserService : BaseService<UserValidators>, IUserService
  {
    private readonly IConfiguration _configuration;

    public UserService(
      IConfiguration configuration,
      IUnitOfWork unitOfWork,
      UserValidators validator,
      ExceptionsLogger logger) : base(unitOfWork, validator, logger)
    {
      _configuration = configuration;
    }

    public async Task<ResponseData<List<User>>> GetUsers()
    {
      var response = new ResponseData<List<User>>();
      try
      {
        response.Data = await _unitOfWork.UserRepository.GetUsers();
        response.Message = ReplyMessage.QUERY_SUCCESS;
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Data = new List<User>();
        response.Message = ReplyMessage.QUERY_EMPTY;
        _logger.SetException("Error al obtener usuarios: " + ex.Message);
      }

      return response;
    }

    public async Task<ResponseData<User>> GetUserById(int id)
    {
      var response = new ResponseData<User>();
      try
      {
        response.Data = await _unitOfWork.UserRepository.GetUserById(id);
        response.Message = ReplyMessage.QUERY_SUCCESS;
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Data = new User();
        response.Message = ReplyMessage.QUERY_EMPTY;
        _logger.SetException("Error al obtener usuario por id: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> InsertUser(User user)
    {
      var response = new Response();
      var validationResult = await _validator.ValidaUser(user);
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
        response.Message = ReplyMessage.SAVE;
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = ReplyMessage.FAILED;
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
        response.IsSuccess = false;
        response.Message = ReplyMessage.RECORD_NOT_FOUND;
        return response;
      }

      var validationResult = await _validator.ValidaUser(user);
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
        response.Message = ReplyMessage.UPDATE;
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = ReplyMessage.FAILED;
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
        response.IsSuccess = false;
        response.Message = ReplyMessage.RECORD_NOT_FOUND;
        return response;
      }

      try
      {
        await _unitOfWork.UserRepository.DeleteUser(id);
        _unitOfWork.Commit();
        response.Message = ReplyMessage.DELETE;
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = ReplyMessage.FAILED;
        _logger.SetException("Error al eliminar usuario: " + ex.Message);
      }

      return response;
    }

    public async Task<ResponseData<CredentialsDTO>> Login(LoginDTO login)
    {
      var response = new ResponseData<CredentialsDTO>();
      var validationResult = await _validator.ValidaLogin(login);
      if (!validationResult.IsValid)
      {
        response.NotValid(validationResult.Errors);
        return response;
      }

      try
      {
        var user = await _unitOfWork.UserRepository.GetByUserName(login.UserName);

        if (user is null || !BC.Verify(login.Password, user.Password))
        {
          response.IsSuccess = false;
          response.Message = ReplyMessage.LOGIN_ERROR;
          return response;
        }

        response.IsSuccess = true;
        response.Message = ReplyMessage.LOGIN_SUCCESS;
        response.Data.Token = GenerateToken();
      }
      catch (Exception ex)
      {
        response.IsSuccess = false;
        response.Message = ReplyMessage.FAILED;
        _logger.SetException("Error en el proceso de Login: " + ex.Message);
      }

      return response;
    }

    private string GenerateToken()
    {
      var jwtKey = _configuration["Keys:JWTKey"]!;
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.NameId, "samc9201@hotmail.com"),
        new Claim(JwtRegisteredClaimNames.FamilyName, "admin"),
        new Claim(JwtRegisteredClaimNames.GivenName, "samc9201@hotmail.com"),
        new Claim(JwtRegisteredClaimNames.UniqueName, "1"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, Guid.NewGuid().ToString(), ClaimValueTypes.Integer64),
      };

      var token =
        new JwtSecurityToken(
          issuer: "www.saul.com.mx",
          audience: "www.saul.com.mx",
          claims: claims,
          expires: DateTime.UtcNow.AddSeconds(10),
          notBefore: DateTime.UtcNow,
          signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
