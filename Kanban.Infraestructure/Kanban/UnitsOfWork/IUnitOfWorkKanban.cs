using Kanban.Infraestructure.Kanban.Interfaces;

namespace Kanban.Infraestructure.Kanban.UnitsOfWork
{
    public interface IUnitOfWorkKanban : IDisposable
    {
        public IFeatureRepository FeatureRepository { get; }
        public IGroupProjectRepository GroupProjectRepository { get; }
        public IUserRepository UserRepository { get; }
        public void Commit();
    }
}
