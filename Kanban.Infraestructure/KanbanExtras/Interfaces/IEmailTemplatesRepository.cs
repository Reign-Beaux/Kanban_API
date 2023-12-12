using Kanban.Domain.Entities.KanbanExtras;

namespace Kanban.Infraestructure.KanbanExtras.Interfaces
{
  public interface IEmailTemplatesRepository
  {
    Task<EmailTemplates> GetByCode(string code);
  }
}
