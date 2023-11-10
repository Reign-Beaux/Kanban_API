﻿using Kanban.Application.Common.DTOs.Login.Request;
using Kanban.Application.Common.DTOs.Login.Response;
using Kanban.Application.Common.Models;
using Kanban.Application.Common.Statics;
using Kanban.Application.Common.Utils;
using Kanban.Application.Interfaces;
using Kanban.Application.Validators.Login;
using Kanban.Infraestructure.UnitsOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace Kanban.Application.Services
{
  public class LoginService : BaseService, ILoginService
  {
    private readonly IConfiguration _configuration;
    private protected readonly LoginValidator _validator;

    public LoginService(
                        IConfiguration configuration,
                        IUnitOfWork unitOfWork,
                        LoginValidator validator,
                        ExceptionsLogger logger) : base(unitOfWork, logger)
    {
      _configuration = configuration;
      _validator = validator;
    }

    public async Task<ResponseData<CredentialsDTO>> Authenticate(LoginDTO login)
    {
      var response = new ResponseData<CredentialsDTO>();
      var validationResult = await _validator.ExecuteValidateLogin(login);
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
          response.Status = StatusResponse.NOT_FOUND;
          response.Message = ReplyMessages.LOGIN_ERROR;
        }
        else
        {
          response.Message = ReplyMessages.LOGIN_SUCCESS;
          response.Data.Token = GenerateToken();
        }
      }
      catch (Exception ex)
      {
        response.Status = StatusResponse.INTERNAL_SERVER_ERROR;
        response.Message = ReplyMessages.FAILED_OPERATION;
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
