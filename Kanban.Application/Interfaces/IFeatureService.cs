using Kanban.Application.Common.Models;
using Kanban.Domain.Entities;

namespace Kanban.Application.Interfaces
{
  public interface IFeatureService
  {
    Task<ResponseData<List<Feature>>> GetFeatures();
    Task<ResponseData<Feature>> GetFeatureById(int id);
    Task<Response> InsertFeature(Feature feature);
    Task<Response> UpdateFeature(Feature feature);
    Task<Response> DeleteFeature(int id);
  }
}
