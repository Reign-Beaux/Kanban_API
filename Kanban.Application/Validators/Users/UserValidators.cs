using FluentValidation.Results;
using Kanban.Application.DTOs.Users.Request;
using Kanban.Domain.Entities;

namespace Kanban.Application.Validators.Users
{
  public class UserValidators
  {
    private readonly LoginValidator _loginValidator;
    private readonly UserValidator _userValidator;

    public UserValidators(LoginValidator loginValidator, UserValidator userValidator)
    {
      _loginValidator = loginValidator;
      _userValidator = userValidator;
    }

    public async Task<ValidationResult> ValidaLogin (LoginDTO login)
      => await _loginValidator.ValidateAsync(login);

    public async Task<ValidationResult> ValidaUser(User user)
      => await _userValidator.ValidateAsync(user);
  }
}
