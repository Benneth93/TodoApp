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

    public TodoTask CreateNewTodo(string title, string description)
    {
        var task =  _toDoDbContext.Tasks.Add(new()
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

    public TodoTask DeleteTodo(int id)
    {
        var task = _toDoDbContext.Tasks.FirstOrDefault(t => t.TaskID == id);

        if (task != null)
        {
            _toDoDbContext.Tasks.Remove(task);
            _toDoDbContext.SaveChanges();
        }

        return task;
    }

    public TodoTask UpdateTodo(int id, string title, string description)
    {
        var task = _toDoDbContext.Tasks.FirstOrDefault(t => t.TaskID == id);
        
        
        if (task != null)
        {
            task.Description = description;
            task.Title = title;

            _toDoDbContext.SaveChanges();
        }

        return task;
    }
}