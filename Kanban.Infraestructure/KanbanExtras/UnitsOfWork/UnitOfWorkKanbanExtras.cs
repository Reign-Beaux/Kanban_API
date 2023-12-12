using Kanban.Infraestructure.Common;
using Kanban.Infraestructure.KanbanExtras.Interfaces;
using Kanban.Infraestructure.KanbanExtras.Repositories;
using Microsoft.Extensions.Configuration;

namespace Kanban.Infraestructure.KanbanExtras.UnitsOfWork
{
  public class UnitOfWorkKanbanExtras : BaseUnitOfWork, IUnitOfWorkKanbanExtras
  {
    public IEmailTemplatesRepository EmailTemplatesRepository { get; }
    public IRecoveryPasswordRepository RecoveryPasswordRepository { get; }

    public UnitOfWorkKanbanExtras(IConfiguration configuration) : base (configuration["ConnectionStrings:KanbanExtras"]!)
    {
      EmailTemplatesRepository = new EmailTemplatesRepository(_dbTransaction);
      RecoveryPasswordRepository = new RecoveryPasswordRepository(_dbTransaction);
    }
  }
}
