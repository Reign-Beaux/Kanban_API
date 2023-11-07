using Kanban.Domain.Entities;

namespace Kanban.Infraestructure.Interfaces
{
  public interface IGroupProjectRepository
  {
    Task<List<GroupProject>> GetGroupProjects();
    Task<GroupProject> GetGroupProjectById(int id);
    Task InsertGroupProject(GroupProject groupProject);
    Task UpdateGroupProject(GroupProject groupProject);
    Task DeleteGroupProject(int id);
  }
}
