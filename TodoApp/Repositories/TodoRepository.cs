using TodoApp.Data;
using TodoApp.Interfaces;

namespace TodoApp.Repositlries;

public class TodoRepository : IToDoRepository
{
    private ToDoDbContext _toDoDbContext;

    public TodoRepository(ToDoDbContext toDoDbContext)
    {
        _toDoDbContext = toDoDbContext;
    }

    public async Task<TodoTask> CreateNewTodo(string title, string description)
    {
        var task = await _toDoDbContext.Tasks.AddAsync(new()
        {
            Title = title,
            Description = description
        });
        _toDoDbContext.SaveChanges();
        return task.Entity;
    }

    public IEnumerable<TodoTask> GetAllTasks()
    {
        return _toDoDbContext.Tasks.OrderBy(t => t.TaskID);
    }
}