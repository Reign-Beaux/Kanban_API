using Kanban.Application.Common.DTOs.Login.Request;
using Kanban.Application.Common.DTOs.Login.Response;
using Kanban.Application.Common.Models;

namespace Kanban.Application.Interfaces
{
    public interface ILoginService
    {
        Task<ResponseData<CredentialsDTO>> Authenticate(LoginDTO login);
    }
}
