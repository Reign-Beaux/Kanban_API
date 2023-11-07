using Dapper;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.Interfaces;
using System.Data;

namespace Kanban.Infraestructure.Repositories
{
  public class FeatureRepository : BaseRepository, IFeatureRepository
  {
    public FeatureRepository(IDbTransaction dbTransaction) : base(dbTransaction)
    {
    }

    public async Task<List<Feature>> GetFeatures()
    {
      var spString = "[dbo].[usp_Features_GET]";
      return (await _dbConnection.QueryAsync<Feature>(spString, transaction: _dbTransaction)).ToList();
    }

    public async Task<Feature> GetFeatureById(int id)
    {
      var spString = "[dbo].[usp_Features_GET] @Id";
      return await _dbConnection.QuerySingleOrDefaultAsync<Feature>(
        spString,
        new { Id = id },
        transaction: _dbTransaction);
    }

    public async Task InsertFeature(Feature feature)
    {
      var spString = "[dbo].[usp_Features_INS] @Name";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          new { feature.Name },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to INSERT Feature: " + ex.Message);
      }
    }

    public async Task UpdateFeature(Feature feature)
    {
      var spString = "[dbo].[usp_Features_UPD] @Id, @Name";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          feature,
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to INSERT Feature: " + ex.Message);
      };
    }

    public async Task DeleteFeature(int id)
    {
      var spString = "[dbo].[Usp_Features_DEL] @Id";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          new { Id = id },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to DELETE Feature: " + ex.Message);
      }
    }
  }
}
