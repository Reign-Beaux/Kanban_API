using Kanban.Infraestructure.Interfaces;

namespace Kanban.Infraestructure.UnitsOfWork
{
  public interface IUnitOfWork : IDisposable
  {
    public IFeatureRepository FeatureRepository { get; }
    public IGroupProjectRepository GroupProjectRepository { get; }
    public IUserRepository UserRepository { get; }
    public void Commit();
  }
}
