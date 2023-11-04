namespace Kanban.Application.Common.Interfaces
{
    public interface IResponse
    {
        int Status { get; set; }
        string Message { get; set; }
    }
}
