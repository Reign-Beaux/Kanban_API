using Kanban.Application.Common.Utils;
using Kanban.Infraestructure.Kanban.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class BaseService
  {
    private protected readonly ExceptionsLogger _logger;

    public BaseService(ExceptionsLogger logger)
    {
      _logger = logger;
    }
  }
}
