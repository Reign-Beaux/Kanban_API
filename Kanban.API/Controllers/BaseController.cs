using Kanban.Application.Common.Interfaces;
using Kanban.Application.Common.Statics;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.Controllers
{
  public class BaseController<T> : ControllerBase
  {
    protected readonly T _service;

    public BaseController(T service)
    {
      _service = service;
    }

    protected IActionResult HandleResponse(IResponse response)
    {
      Dictionary<int, Func<IResponse, IActionResult>> responseDictionary = new()
      {
          { StatusResponse.OK, response => Ok(response) },
          { StatusResponse.BAD_REQUEST, response => BadRequest(response) },
          { StatusResponse.NOT_FOUND, response => NotFound(response) },
          { StatusResponse.INTERNAL_SERVER_ERROR, response => StatusCode(500, response.Message) },
      };

      return responseDictionary[response.Status](response);
    }
  }
}
