using FluentValidation.Results;
using Kanban.Application.DTOs.Users.Request;

namespace Kanban.Application.Validators.Users
{
  public class UserValidators
  {
    private readonly LoginValidator _loginValidator;

    public UserValidators(LoginValidator loginValidator)
    {
      _loginValidator = loginValidator;
    }

    public async Task<ValidationResult> Login (LoginDTO login)
      => await _loginValidator.ValidateAsync(login);
  }
}
