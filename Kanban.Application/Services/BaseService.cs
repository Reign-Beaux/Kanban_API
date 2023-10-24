using Kanban.Infraestructure.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class BaseService
  {
    private protected readonly IUnitOfWork _unitOfWork;

    public BaseService(IUnitOfWork unitOfWork)
      => _unitOfWork = unitOfWork;
  }
}
