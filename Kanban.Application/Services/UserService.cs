using Kanban.Domain.IServices;
using Kanban.Infraestructure.UnitsOfWork;

namespace Kanban.Application.Services
{
  public class UserService : BaseService, IUserService
  {
    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
  }
}
