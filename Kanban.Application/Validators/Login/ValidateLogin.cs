using FluentValidation;
using Kanban.Application.DTOs.Login.Request;

namespace Kanban.Application.Validators.Login
{
  public class ValidateLogin : AbstractValidator<LoginDTO>
  {
    public ValidateLogin()
    {
      RuleFor(login => login.UserName)
        .NotEmpty().WithMessage("El Usuario es un campo requerido.");

      RuleFor(login => login.Password)
        .NotEmpty().WithMessage("La Contraseña es un campo requerido.");
    }
  }
}
