using Kanban.Application.DTOs.Users.Request;
using Kanban.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.Controllers
{
    [Route("api/[controller]")]
  [ApiController]
  public class UserController : BaseController
  {
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
      _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO login)
    {
      var response = await _service.Login(login);
      return HandleResponse(response);
    }
  }
}
