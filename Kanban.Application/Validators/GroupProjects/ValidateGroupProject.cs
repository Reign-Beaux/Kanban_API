using FluentValidation;
using Kanban.Domain.Entities;

namespace Kanban.Application.Validators.GroupProjects
{
  public class ValidateGroupProject : AbstractValidator<GroupProject>
  {
    public ValidateGroupProject()
    {
      RuleFor(groupProject => groupProject.Name)
        .NotEmpty().WithMessage("El Nombre es un campo requerido.");
    }
  }
}
