namespace TooDoo.Models;

public class TodoList
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<TodoItem> TodoItems { get; set; } = new();
}
