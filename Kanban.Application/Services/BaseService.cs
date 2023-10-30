using Kanban.Application.Utils;
using Kanban.Infraestructure.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class BaseService<T>
  {
    private protected readonly IUnitOfWork _unitOfWork;
    private protected readonly T _validator;
    private protected readonly ExceptionsLogger _logger;

    public BaseService(IUnitOfWork unitOfWork, T validator, ExceptionsLogger logger)
    {
      _unitOfWork = unitOfWork;
      _validator = validator;
      _logger = logger;
    }
  }
}
