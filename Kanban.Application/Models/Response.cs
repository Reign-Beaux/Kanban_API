using Kanban.Application.Interfaces.Models;

namespace Kanban.Application.Models
{
    public class Response : IResponse
  {
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; }
  }
}
