using Kanban.Application.DTOs.Login.Request;
using Kanban.Application.DTOs.Login.Response;
using Kanban.Application.Models;

namespace Kanban.Application.Interfaces.Services
{
  public interface ILoginService
  {
    Task<ResponseData<CredentialsDTO>> Authenticate(LoginDTO login);
  }
}
