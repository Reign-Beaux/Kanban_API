using Kanban.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FeatureController : BaseController<IFeatureService>
  {
    public FeatureController(IFeatureService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetFeatures()
    {
      var response = await _service.GetFeatures();
      return HandleResponse(response);
    }
  }
}
