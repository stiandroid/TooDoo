using TooDoo.Enums;

namespace TooDoo.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public PriorityEnum Priority { get; set; }
    public int SortOrder { get; set; } // Sortering innen samme prioritet
    public bool IsDone { get; set; }

    public Guid? TodoListId { get; set; }
    public TodoList? TodoList { get; set; }
}
