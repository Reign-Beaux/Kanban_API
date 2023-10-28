using FluentValidation;
using Kanban.Domain.Entities;

namespace Kanban.Application.Validators.Users
{
  public class UserValidator : AbstractValidator<User>
  {
    public UserValidator()
    {
      RuleFor(user => user.FullName)
        .NotEmpty().WithMessage("El campo Nombre es requerido.");
      RuleFor(user => user.Username)
        .NotEmpty().WithMessage("El campo Usuario es requerido.");
      RuleFor(user => user.Email)
        .NotEmpty().WithMessage("El campo email es requerido.");
      RuleFor(user => user.Password)
        .NotEmpty().WithMessage("El campo Contraseña es requerido");
    }
  }
}
