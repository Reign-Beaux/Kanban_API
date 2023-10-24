using Kanban.Domain.IRepositories;
using System.Data;

namespace Kanban.Infraestructure.Repositories
{
  public class UserRepository : BaseRepository, IUserRepository
  {
    public UserRepository(IDbTransaction dbTransaction) : base(dbTransaction)
    {
    }
  }
}
