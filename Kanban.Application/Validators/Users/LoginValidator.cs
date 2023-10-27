using FluentValidation;
using Kanban.Application.DTOs.Users.Request;

namespace Kanban.Application.Validators.Users
{
  public class LoginValidator : AbstractValidator<LoginDTO>
  {
    public LoginValidator()
    {
      RuleFor(login => login.UserName)
        .NotEmpty().WithMessage("El Usuario es un campo requerido.");
      RuleFor(login => login.Password)
        .NotEmpty().WithMessage("La Contraseña es un campo requerido.");
    }
  }
}
