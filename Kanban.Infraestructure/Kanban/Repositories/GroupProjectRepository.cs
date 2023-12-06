using Dapper;
using Kanban.Domain.Entities;
using Kanban.Infraestructure.Common;
using Kanban.Infraestructure.Kanban.Interfaces;
using System.Data;

namespace Kanban.Infraestructure.Kanban.Repositories
{
  public class GroupProjectRepository : BaseRepository, IGroupProjectRepository
  {
    public GroupProjectRepository(IDbTransaction dbTransaction) : base(dbTransaction)
    {
    }

    public async Task<List<GroupProject>> GetGroupProjects()
    {
      var spString = "[dbo].[usp_GroupProjects_GET]";
      return (await _dbConnection.QueryAsync<GroupProject>(spString, transaction: _dbTransaction)).ToList();
    }

    public async Task<GroupProject> GetGroupProjectById(int id)
    {
      var spString = "[dbo].[usp_GroupProjects_GET] @Id";
      return await _dbConnection.QuerySingleOrDefaultAsync<GroupProject>(
        spString,
        new { Id = id },
        transaction: _dbTransaction);
    }

    public async Task InsertGroupProject(GroupProject groupProject)
    {
      var spString = "[dbo].[usp_GroupProjects_INS] @Name";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          new { groupProject.Name },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to INSERT GroupProject: " + ex.Message);
      }
    }

    public async Task UpdateGroupProject(GroupProject groupProject)
    {
      var spString = "[dbo].[usp_GroupProjects_UPD] @Id, @Name";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          groupProject,
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to INSERT GroupProject: " + ex.Message);
      };
    }

    public async Task DeleteGroupProject(int id)
    {
      var spString = "[dbo].[usp_GroupProjects_DEL] @Id";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          new { Id = id },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to DELETE GroupProject: " + ex.Message);
      }
    }
  }
}
