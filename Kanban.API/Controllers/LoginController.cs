using Kanban.Application.DTOs.Login.Request;
using Kanban.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoginController : BaseController
  {
    private readonly ILoginService _service;

    public LoginController(ILoginService service)
    {
      _service = service;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO login)
    {
      var response = await _service.Authenticate(login);
      return HandleResponse(response);
    }
  }
}
