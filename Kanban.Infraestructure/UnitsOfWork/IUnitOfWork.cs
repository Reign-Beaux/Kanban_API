using Kanban.Domain.IRepositories;

namespace Kanban.Infraestructure.UnitsOfWork
{
  public interface IUnitOfWork : IDisposable
  {
    public interface IUnitOfWork : IDisposable
    {
      public IUserRepository UserRepository { get; }
      public void Commit();
    }
  }
}
