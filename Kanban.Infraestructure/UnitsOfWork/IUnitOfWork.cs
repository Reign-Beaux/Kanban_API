using Kanban.Infraestructure.Interfaces;

namespace Kanban.Infraestructure.UnitsOfWork
{
  public interface IUnitOfWork : IDisposable
  {
    public IUserRepository UserRepository { get; }
    public void Commit();
  }
}
