using System.Data;

namespace Kanban.Infraestructure.Repositories
{
  public class BaseRepository
  {
    private protected readonly IDbTransaction _dbTransaction;
    private protected readonly IDbConnection _dbConnection;

    public BaseRepository(IDbTransaction dbTransaction)
    {
      _dbTransaction = dbTransaction;
      _dbConnection = dbTransaction.Connection!;
    }
  }
}
