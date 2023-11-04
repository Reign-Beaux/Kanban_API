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

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO login)
    {
      var response = await _service.Authenticate(login);
      return HandleResponse(response);
    }
  }
}
