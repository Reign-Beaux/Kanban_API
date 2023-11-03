namespace Kanban.Domain.Entities
{
  public class Todo
  {
    public int Id { get; set; }
    public int AssignedId { get; set; }
    public int TesterId { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Comments { get; set; }
  }
}
