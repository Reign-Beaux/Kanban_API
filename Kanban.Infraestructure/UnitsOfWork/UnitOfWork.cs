using Kanban.Infraestructure.Interfaces;
using Kanban.Infraestructure.Repositories;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Kanban.Infraestructure.UnitsOfWork
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly IDbConnection _dbConnection;
    private readonly IDbTransaction _dbTransaction;

    public IUserRepository UserRepository { get; }

    public UnitOfWork(IConfiguration configuration)
    {
      _dbConnection = new SqlConnection(configuration["ConnectionStrings:DefaultConnection"]!);
      _dbConnection.Open();
      _dbTransaction = _dbConnection.BeginTransaction();

      UserRepository = new UserRepository(_dbTransaction);
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

    ~UnitOfWork()
    {
      Dispose(false);

    }
  }
}
