using Kanban.Application.DTOs.Users.Request;
using Kanban.Application.DTOs.Users.Response;
using Kanban.Application.Interfaces.Services;
using Kanban.Application.Models;
using Kanban.Application.Statics;
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
      UserValidators validator) : base(unitOfWork, validator)
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
        response.Data = new List<User>();
        response.Message = ReplyMessage.QUERY_EMPTY;
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
        response.Data = null;
        response.Message = ReplyMessage.QUERY_EMPTY;
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
      }
      catch (Exception ex)
      {
        throw new Exception("Error al insertar usuario: " + ex.Message);
      }

      return response;
    }

    public async Task<Response> UpdateUser(User user)
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
        await _unitOfWork.UserRepository.UpdateUser(user);
        _unitOfWork.Commit();
        return response;
      }
      catch (Exception ex)
      {
        throw new Exception("Error al insertar usuario: " + ex.Message);
      }
    }

    public async Task<Response> DeleteUser(int id)
    {
      throw new NotImplementedException();
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

        return response;
      }
      catch (Exception ex)
      {
        throw new Exception("Error de inicio de sesión: " + ex.Message);
      }
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
