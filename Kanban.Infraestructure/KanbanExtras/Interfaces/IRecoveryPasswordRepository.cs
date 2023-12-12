using Kanban.Domain.Entities.KanbanExtras;

namespace Kanban.Infraestructure.KanbanExtras.Interfaces
{
  public interface IRecoveryPasswordRepository
  {
    Task InsertRecord(string stringCode, int idUser);
    Task<RecoveryPassword> GetRecord(string stringCode);
    Task DeleteRecord(string stringCode);
  }
}
