using Kanban.Infraestructure.Kanban.Interfaces;
using Kanban.Infraestructure.Kanban.Repositories;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Kanban.Infraestructure.Kanban.UnitsOfWork
{
  public class UnitOfWorkKanban : IUnitOfWorkKanban
  {
    private readonly IDbConnection _dbConnection;
    private readonly IDbTransaction _dbTransaction;

    public IFeatureRepository FeatureRepository { get; }
    public IGroupProjectRepository GroupProjectRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWorkKanban(IConfiguration configuration)
    {
      _dbConnection = new SqlConnection(configuration["ConnectionStrings:Kanban"]!);
      _dbConnection.Open();
      _dbTransaction = _dbConnection.BeginTransaction();

      FeatureRepository = new FeatureRepository(_dbTransaction);
      GroupProjectRepository = new GroupProjectRepository(_dbTransaction);
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

    ~UnitOfWorkKanban()
    {
      Dispose(false);

    }
  }
}
