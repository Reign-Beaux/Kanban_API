using Kanban.Application.Interfaces;
using Kanban.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GroupProjectController : BaseController<IGroupProjectService>
  {
    public GroupProjectController(IGroupProjectService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetGroupProjects()
    {
      var response = await _service.GetGroupProjects();
      return HandleResponse(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGroupProjectById(int id)
    {
      var response = await _service.GetGroupProjectById(id);
      return HandleResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> InsertGroupProject(GroupProject groupProject)
    {
      var response = await _service.InsertGroupProject(groupProject);
      return HandleResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGroupProject(GroupProject groupProject)
    {
      var response = await _service.UpdateGroupProject(groupProject);
      return HandleResponse(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGroupProject(int id)
    {
      var response = await _service.DeleteGroupProject(id);
      return HandleResponse(response);
    }
  }
}
