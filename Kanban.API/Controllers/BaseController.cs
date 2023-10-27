using Kanban.Application.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.Controllers
{
    [Route("api/[controller]")]
  [ApiController]
  public class BaseController : ControllerBase
  {
    protected IActionResult HandleResponse(IResponse response)
    {
      if (response.IsSuccess)
      {
        return Ok(response);
      }

      return BadRequest(response);
    }
  }
}
