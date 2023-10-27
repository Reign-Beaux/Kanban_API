using Kanban.Application.DTOs.Users.Request;
using Kanban.Application.Interfaces.Services;
using Kanban.Domain.Entities;
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

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      var response = await _service.GetUsers();
      return HandleResponse(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
      var response = await _service.GetUserById(id);
      return HandleResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> InsertUser(User user)
    {
      var response = await _service.InsertUser(user);
      return HandleResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(User user)
    {
      var response = await _service.UpdateUser(user);
      return HandleResponse(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      var response = await _service.DeleteUser(id);
      return HandleResponse(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO login)
    {
      var response = await _service.Login(login);
      return HandleResponse(response);
    }
  }
}
