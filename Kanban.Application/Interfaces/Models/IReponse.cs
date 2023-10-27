namespace Kanban.Application.Interfaces.Models
{
    public interface IResponse
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
