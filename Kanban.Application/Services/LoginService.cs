using Kanban.Application.Common.DTOs.Login.Request;
using Kanban.Application.Common.DTOs.Login.Response;
using Kanban.Application.Common.Models;
using Kanban.Application.Common.Statics;
using Kanban.Application.Common.Utils;
using Kanban.Application.Interfaces;
using Kanban.Application.Validators.Login;
using Kanban.Infraestructure.Kanban.UnitsOfWork;
using Kanban.Infraestructure.KanbanExtras.UnitsOfWork;
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
    private readonly IUnitOfWorkKanban _unitOfWorkKanban;
    private readonly IUnitOfWorkKanbanExtras _unitOfWorkKanbanExtras;
    private readonly LoginValidator _validator;
    private readonly EmailSender _emailSender;

    public LoginService(
      IConfiguration configuration,
      IUnitOfWorkKanban unitOfWork,
      IUnitOfWorkKanbanExtras unitOfWorkKanbanExtras,
      LoginValidator validator,
      ExceptionsLogger logger,
      EmailSender emailSender) : base(logger)
    {
      _configuration = configuration;
      _unitOfWorkKanban = unitOfWork;
      _validator = validator;
      _emailSender = emailSender;
      _unitOfWorkKanbanExtras = unitOfWorkKanbanExtras;
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
        var user = await _unitOfWorkKanban.UserRepository.GetByUserName(login.UserName);

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

    public async Task<Response> RecoverPassword(string userName)
    {
      var response = new Response();

      try
      {
        var user = await _unitOfWorkKanban.UserRepository.GetByUserName(userName);

        if (user is null)
        {
          response.Status = StatusResponse.NOT_FOUND;
          response.Message = ReplyMessages.LOGIN_ERROR;
        }
        else
        {

          var codeTemplate = _configuration["EmailTemplates:RecoverPassword"]!;
          var template = await _unitOfWorkKanbanExtras.EmailTemplatesRepository.GetByCode(codeTemplate);
          var newTemplate = template.Html.Replace("[FullName]", user.FullName);
          _emailSender.SendEmail(user.Email, EmailSubject.RECOVER_PASSWORD, newTemplate);
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
  }
}
