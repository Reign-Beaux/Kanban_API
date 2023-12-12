using Kanban.Application.Common.DTOs.Login.Request;
using Kanban.Application.Interfaces;
using Kanban.Domain.Entities.KanbanExtras;
using Kanban.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoginController : BaseController<ILoginService>
  {
    public LoginController(ILoginService service) : base(service)
    {
    }

    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginDTO login)
    {
      var response = await _service.Authenticate(login);
      return HandleResponse(response);
    }

    [HttpPost("RecoverPassword")]
    public async Task<IActionResult> RecoverPassword([FromBody] OnlyString request)
    {
      var response = await _service.RecoverPassword(request.Parameter);
      return HandleResponse(response);
    }

    [HttpPost("RecoverPasswordStep2")]
    public async Task<IActionResult> RecoverPasswordStep2([FromBody] OnlyString request)
    {
      var response = await _service.RecoverPasswordStep2(request.Parameter);
      return HandleResponse(response);
    }
  }
}
