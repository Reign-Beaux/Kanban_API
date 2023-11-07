using FluentValidation.Results;
using Kanban.Application.Common.DTOs.Login.Request;

namespace Kanban.Application.Validators.Login
{
  public class LoginValidator
  {
    private readonly ValidateLogin _validateLogin;

    public LoginValidator(ValidateLogin validateLogin)
    {
      _validateLogin = validateLogin;
    }

    public async Task<ValidationResult> ExecuteValidateLogin(LoginDTO login)
      => await _validateLogin.ValidateAsync(login);
  }
}
