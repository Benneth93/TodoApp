using TodoApp.Data;
using Task = System.Threading.Tasks.Task;

namespace TodoApp.Services;

public class TodoService
{
    private ToDoDbContext _toDoDbContext = new();
    
    public async Task CreateNewTask(string title, string description)
    {
        _toDoDbContext.Tasks.AddAsync(new()
        {
            Title = title,
            Description = description
        });
    }
}