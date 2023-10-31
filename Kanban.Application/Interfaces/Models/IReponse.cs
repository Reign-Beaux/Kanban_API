namespace Kanban.Application.Interfaces.Models
{
    public interface IResponse
    {
        int Status { get; set; }
        string Message { get; set; }
    }
}
