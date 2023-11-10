using FluentValidation;
using Kanban.Domain.Entities;
using System.Text.RegularExpressions;

namespace Kanban.Application.Validators.Users
{
  public class ValidateUser : AbstractValidator<User>
  {
    public ValidateUser()
    {
      RuleFor(user => user.RoleId)
        .NotNull().WithMessage("Debe seleccionar un Rol")
        .NotEqual(0).WithMessage("Debe seleccionar un Rol");

      RuleFor(user => user.FullName)
        .NotEmpty().WithMessage("El campo Nombre es requerido.")
        .Matches("^[^0-9]*$").WithMessage("El campo Nombre no debe contener números");

      RuleFor(user => user.UserName)
        .NotEmpty().WithMessage("El campo Usuario es requerido.");

      RuleFor(user => user.Email)
        .NotEmpty().WithMessage("El campo Correo electrónico es requerido.")
        .Matches("^[A-Z0-9._\\-]+@[A-Z0-9.-]+\\.[A-Z]{2,}$", RegexOptions.IgnoreCase)
        .WithMessage("La dirección del Correo electrónico es incorrecta.");

      RuleFor(user => user.Password)
        .NotEmpty().WithMessage("El campo Contraseña es requerido");
    }
  }
}
