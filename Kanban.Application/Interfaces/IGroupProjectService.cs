using Kanban.Application.Common.Models;
using Kanban.Domain.Entities;

namespace Kanban.Application.Interfaces
{
  public interface IGroupProjectService
  {
    Task<ResponseData<List<GroupProject>>> GetGroupProjects();
    Task<ResponseData<GroupProject>> GetGroupProjectById(int id);
    Task<Response> InsertGroupProject(GroupProject groupProject);
    Task<Response> UpdateGroupProject(GroupProject groupProject);
    Task<Response> DeleteGroupProject(int id);
  }
}
