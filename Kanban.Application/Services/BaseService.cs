using Kanban.Infraestructure.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class BaseService<T>
  {
    private protected readonly IUnitOfWork _unitOfWork;
    private protected readonly T _validator;

    public BaseService(IUnitOfWork unitOfWork, T validator)
    {
      _unitOfWork = unitOfWork;
      _validator = validator;
    }
  }
}
