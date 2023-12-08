using Kanban.Application.Common.DTOs.Login.Request;
using Kanban.Application.Interfaces;
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
    public async Task<IActionResult> RecoverPassword([FromBody] string userName)
    {
      var response = await _service.RecoverPassword(userName);
      return HandleResponse(response);
    }
  }
}
