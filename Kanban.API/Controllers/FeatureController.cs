using Kanban.Application.Interfaces;
using Kanban.Domain.Entities;
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetFeatureById(int id)
    {
      var response = await _service.GetFeatureById(id);
      return HandleResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> InsertFeature(Feature feature)
    {
      var response = await _service.InsertFeature(feature);
      return HandleResponse(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFeature(Feature feature)
    {
      var response = await _service.UpdateFeature(feature);
      return HandleResponse(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFeature(int id)
    {
      var response = await _service.DeleteFeature(id);
      return HandleResponse(response);
    }
  }
}
