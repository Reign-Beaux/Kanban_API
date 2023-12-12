using Dapper;
using Kanban.Domain.Entities;
using Kanban.Domain.Entities.KanbanExtras;
using Kanban.Infraestructure.Common;
using Kanban.Infraestructure.KanbanExtras.Interfaces;
using System.Data;

namespace Kanban.Infraestructure.KanbanExtras.Repositories
{
  public class EmailTemplatesRepository : BaseRepository, IEmailTemplatesRepository
  {
    public EmailTemplatesRepository(IDbTransaction dbTransaction) : base(dbTransaction)
    {
    }

    public async Task<EmailTemplates> GetByCode(string code)
    {
      var spString = "[dbo].[Usp_EmailTemplates_GET] @Code";
      return await _dbConnection.QuerySingleOrDefaultAsync<EmailTemplates>(
        spString,
        new { Code = code },
        transaction: _dbTransaction);
    }
  }
}
