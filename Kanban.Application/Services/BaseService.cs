using Kanban.Application.Common.Utils;
using Kanban.Infraestructure.UnitsOfWork;

namespace Kanban.Application.Services
{
    public class BaseService
  {
    private protected readonly IUnitOfWork _unitOfWork;
    private protected readonly ExceptionsLogger _logger;

    public BaseService(IUnitOfWork unitOfWork, ExceptionsLogger logger)
    {
      _unitOfWork = unitOfWork;
      _logger = logger;
    }
  }
}
