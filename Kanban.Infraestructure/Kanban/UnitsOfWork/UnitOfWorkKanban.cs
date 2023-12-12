using Kanban.Infraestructure.Common;
using Kanban.Infraestructure.Kanban.Interfaces;
using Kanban.Infraestructure.Kanban.Repositories;
using Microsoft.Extensions.Configuration;

namespace Kanban.Infraestructure.Kanban.UnitsOfWork
{
  public class UnitOfWorkKanban : BaseUnitOfWork, IUnitOfWorkKanban
  {
    public IFeatureRepository FeatureRepository { get; }
    public IGroupProjectRepository GroupProjectRepository { get; }
    public IUserRepository UserRepository { get; }

    public UnitOfWorkKanban(IConfiguration configuration) : base (configuration["ConnectionStrings:Kanban"]!)
    {

      FeatureRepository = new FeatureRepository(_dbTransaction);
      GroupProjectRepository = new GroupProjectRepository(_dbTransaction);
      UserRepository = new UserRepository(_dbTransaction);
    }
  }
}
