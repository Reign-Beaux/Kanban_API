namespace Kanban.Domain.Entities
{
  public class TodoDetail
  {
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int TaskDetailStatusId { get; set; }
    public string Description { get; set; }
    public string Comments { get; set; }
    public string Observations { get; set; }
  }
}
