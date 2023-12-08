using Kanban.Infraestructure.KanbanExtras.Interfaces;

namespace Kanban.Infraestructure.KanbanExtras.UnitsOfWork
{
  public interface IUnitOfWorkKanbanExtras : IDisposable
  {
    public IEmailTemplatesRepository EmailTemplatesRepository { get; }
    public void Commit();
  }
}
