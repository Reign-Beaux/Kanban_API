using FluentValidation.Results;
using Kanban.Application.Interfaces.Models;
using Kanban.Application.Statics;
using System.ComponentModel.DataAnnotations;

namespace Kanban.Application.Models
{
    public class Response : IResponse
  {
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; }
    public IEnumerable<ValidationFailure> Errors { get; set; }

    public void NotValid(IEnumerable<ValidationFailure> errors)
    {
      IsSuccess = false;
      Message = ReplyMessage.VALIDATE;
      Errors = errors;
    }
  }
}
