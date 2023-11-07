using FluentValidation.Results;
using Kanban.Domain.Entities;

namespace Kanban.Application.Validators.GroupProjects
{
  public class GroupProjectValidator
  {
    private readonly ValidateGroupProject _validateGroupProject;

    public GroupProjectValidator(ValidateGroupProject validateGroupProject)
    {
      _validateGroupProject = validateGroupProject;
    }

    public async Task<ValidationResult> ExecuteValidateGroupProject(GroupProject groupProject)
      => await _validateGroupProject.ValidateAsync(groupProject);
  }
}
