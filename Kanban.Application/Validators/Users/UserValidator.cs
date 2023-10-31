using FluentValidation.Results;
using Kanban.Domain.Entities;

namespace Kanban.Application.Validators.Users
{
  public class UserValidator
  {
    private readonly ValidateUser _userValidator;

    public UserValidator(ValidateUser userValidator)
    {
      _userValidator = userValidator;
    }

    public async Task<ValidationResult> ExecuteValidateUser(User user)
      => await _userValidator.ValidateAsync(user);
  }
}
