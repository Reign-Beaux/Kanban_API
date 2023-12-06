using Kanban.Domain.Entities;

namespace Kanban.Infraestructure.Kanban.Interfaces
{
  public interface IFeatureRepository
  {
    Task<List<Feature>> GetFeatures();
    Task<Feature> GetFeatureById(int id);
    Task InsertFeature(Feature feature);
    Task UpdateFeature(Feature feature);
    Task DeleteFeature(int id);
  }
}
