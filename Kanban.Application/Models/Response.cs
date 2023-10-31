using FluentValidation.Results;
using Kanban.Application.Interfaces.Models;
using Kanban.Application.Statics;

namespace Kanban.Application.Models
{
  public class Response : IResponse
  {
    public int Status { get; set; } = StatusResponse.OK;
    public string Message { get; set; }
    public IEnumerable<ValidationFailure> Errors { get; set; }

    public void NotValid(IEnumerable<ValidationFailure> errors)
    {
      Status = StatusResponse.BAD_REQUEST;
      Message = ReplyMessages.VALIDATE;
      Errors = errors;
    }
  }
}
