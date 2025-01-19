using System.Text.RegularExpressions;

namespace TooDoo.Services;

public class TodoService(AppDbContext context, ILogger<TodoService> logger) : ITodoService
{
    private readonly AppDbContext _context = context;
    private readonly ILogger<TodoService> _logger = logger;

    public async Task<TodoItem?> GetTodoItemByIdAsync(string id)
    {
        if (!Guid.TryParse(id, out var guid))
        { 
            return null;
        }

        return await _context.TodoItems.FirstOrDefaultAsync(i => i.Id == guid);
    }

    public async Task<bool> AddOrUpdateTodoItemAsync(TodoItem item)
    {
        try
        {
            var dbItem = await _context.TodoItems.FindAsync(item.Id);
            if (dbItem == null) // insert
            {
                item.Id = Guid.NewGuid();
                await _context.TodoItems.AddAsync(item);
            }
            else // update
            { 
                dbItem.Title = item.Title;
                dbItem.IsDone = item.IsDone;
            }
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("Lagring av gjøremål feilet. {Message}", ex.Message);
            return false;
        }
    }

    public async Task<TodoList?> GetTodoListByIdAsync(string id)
    {
        if (!Guid.TryParse(id, out var guid))
        {
            return null;
        }

        var list = await _context.TodoLists.Include(i => i.TodoItems)
                .FirstOrDefaultAsync(i => i.Id == guid);

        if (list != null)
        {
            list.TodoItems = list.TodoItems
                .OrderBy(p => p.Priority)
                   .ThenBy(s => s.SortOrder)
                .ToList();
        }

        return list;
    }

    public async Task<List<TodoList>> GetTodoListsAsync()
        => await _context.TodoLists
            .Include(i => i.TodoItems)
            .ToListAsync();

    public async Task<bool> AddTodoListAsync(TodoList list)
    {
        try
        {
            list.Id = Guid.NewGuid();
            await _context.TodoLists.AddAsync(list);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError("Opprettelse av ny liste feilet. {Message}", ex.Message);
            return false;
        }
    }

    public async Task<string> GenerateListTitleAsync()
    {
        string baseTitle = "Ny liste";

        var titles = await _context.TodoLists
            .Where(t => t.Title.StartsWith(baseTitle))
            .Select(t => t.Title)
            .ToListAsync();

        var filteredTitles = titles
            .Where(t => Regex.IsMatch(t, $@"^{baseTitle} \d+$"))
            .ToList();

        if (filteredTitles.Count > 0)
        {
            int nextNumber = titles
                .Select(t =>
                {
                    var parts = t.Split(' ');
                    return int.TryParse(parts.Last(), out var number) ? number : 0;
                })
                .Max() + 1;
            return $"{baseTitle} {nextNumber}";
        }

        if (titles.Contains(baseTitle))
        {
            return $"{baseTitle} 1";
        }

        return baseTitle;
    }

    public async Task<bool> UpdateListTitleAsync(TodoList list)
    {
        try
        {
            var dbList = await _context.TodoLists
                .Where(l => l.Id == list.Id)
                .FirstOrDefaultAsync();
            if (dbList != null)
            {
                dbList.Title = list.Title;
                return await _context.SaveChangesAsync() > 0;
                
            }
            _logger.LogError("Listen du prøvde å redigere navnet på finnes ikke i databasen.");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError("Oppdatering av listenavn feilet. {Message}", ex.Message);
            return false;
        }
    }
}
