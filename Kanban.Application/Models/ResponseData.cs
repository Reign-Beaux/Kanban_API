namespace Kanban.Application.Models
{
  public class ResponseData<T> : Response where T : new()
  {
    public T Data { get; set; } = new T();
  }
}
