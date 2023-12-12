using Dapper;
using Kanban.Domain.Entities.KanbanExtras;
using Kanban.Infraestructure.Common;
using Kanban.Infraestructure.KanbanExtras.Interfaces;
using System.Data;

namespace Kanban.Infraestructure.KanbanExtras.Repositories
{
  public class RecoveryPasswordRepository : BaseRepository, IRecoveryPasswordRepository
  {
    public RecoveryPasswordRepository(IDbTransaction dbTransaction) : base(dbTransaction)
    {
    }

    public async Task<RecoveryPassword> GetRecord(string stringCode)
    {
      var spString = "[dbo].[usp_RecoveryPassword_GET] @StringCode";
      return await _dbConnection.QuerySingleOrDefaultAsync<RecoveryPassword>(
        spString,
        new { stringCode },
        transaction: _dbTransaction);
    }

    public async Task InsertRecord(string stringCode, int idUser)
    {
      var spString = "[dbo].[usp_RecoveryPassword_INS] @StringCode, @IdUser";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          new { stringCode, idUser },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to INSERT RecoveryPassword: " + ex.Message);
      }
    }

    public async Task DeleteRecord(string stringCode)
    {
      var spString = "[dbo].[usp_RecoveryPassword_DEL] @StringCode";
      try
      {
        await _dbConnection.ExecuteAsync(
          spString,
          new { stringCode },
          transaction: _dbTransaction);
      }
      catch (Exception ex)
      {
        throw new Exception("Error to DELETE Feature: " + ex.Message);
      }
    }
  }
}
