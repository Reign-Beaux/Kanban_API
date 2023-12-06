using Kanban.Infraestructure.KanbanExtras.Interfaces;
using Kanban.Infraestructure.KanbanExtras.Repositories;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Kanban.Infraestructure.KanbanExtras.UnitsOfWork
{
  public class UnitOfWorkKanbanExtras : IUnitOfWorkKanbanExtras
  {
    private readonly IDbConnection _dbConnection;
    private readonly IDbTransaction _dbTransaction;

    public IEmailTemplatesRepository EmailTemplatesRepository { get; }

    public UnitOfWorkKanbanExtras(IConfiguration configuration)
    {
      _dbConnection = new SqlConnection(configuration["ConnectionStrings:KanbanExtras"]!);
      _dbConnection.Open();
      _dbTransaction = _dbConnection.BeginTransaction();

      EmailTemplatesRepository = new EmailTemplatesRepository(_dbTransaction);
    }

    public void Commit()
    {
      try
      {
        _dbTransaction.Commit();
      }
      catch (Exception)
      {
        _dbTransaction.Rollback();
      }
      finally
      {
        _dbTransaction.Dispose();
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;

      _dbTransaction?.Dispose();
      _dbConnection?.Dispose();
    }

    ~UnitOfWorkKanban()
    {
      Dispose(false);

    }
  }
}
