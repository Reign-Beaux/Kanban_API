using FluentValidation.Results;
using Kanban.Application.Common.DTOs.Login.Request;

namespace Kanban.Application.Validators.Login
{
    public class LoginValidator
  {
    private readonly ValidateLogin _loginValidator;

    public LoginValidator(ValidateLogin loginValidator)
    {
      _loginValidator = loginValidator;
    }

    public async Task<ValidationResult> ExecuteValidateLogin(LoginDTO login)
      => await _loginValidator.ValidateAsync(login);
  }
}
