namespace Kanban.Application.Common.Models
{
    public class ResponseData<T> : Response where T : new()
    {
        public T Data { get; set; } = new T();
    }
}
