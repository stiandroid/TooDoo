using System.Diagnostics.Eventing.Reader;

namespace TooDoo;

public interface ITodoService
{
    // Items
    Task<TodoItem?> GetTodoItemByIdAsync(string id);
    Task<bool> AddOrUpdateTodoItemAsync(TodoItem item);

    // Lists
    Task<TodoList?> GetTodoListByIdAsync(string id);
    Task<List<TodoList>> GetTodoListsAsync();
    Task<bool> AddTodoListAsync(TodoList list);
    Task<string> GenerateListTitleAsync();
    Task<bool> UpdateListTitleAsync(TodoList list);
}
