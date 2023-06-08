using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Dtos;
using TodoApp.Interfaces;

namespace TodoApp.Repositlries;

public class TodoRepository : IToDoRepository
{
    private ToDoDbContext _toDoDbContext;

    public TodoRepository(ToDoDbContext toDoDbContext)
    {
        _toDoDbContext = toDoDbContext;
    }

    public async Task<TodoTask> CreateNewTodo(NewTodoDto newTodoDto)
    {
        var task = await _toDoDbContext.Tasks.AddAsync(new()
        {
            Title = newTodoDto.Title,
            Description = newTodoDto.Description
        });
        await _toDoDbContext.SaveChangesAsync();
            
        return task.Entity;
    }

    public IEnumerable<TodoTask> GetAllTasks()
    {
        return _toDoDbContext.Tasks.OrderBy(t => t.TaskID);
    }

    public TodoTask DeleteTodo(int id)
    {
        var task = _toDoDbContext.Tasks.FirstOrDefault(t => t.TaskID == id);

        if (task == null) return null;
        
        _toDoDbContext.Tasks.Remove(task);
        _toDoDbContext.SaveChanges();

        return task;
    }

    //Todo: Add same validation that's used in create
    //Todo: Add validation to throw 404 not found if the task does not exist
    public async Task<TodoTask?> UpdateTodo(TodoDto updateTask)
    {
        var task = await _toDoDbContext.Tasks.FirstOrDefaultAsync(t => t.TaskID == updateTask.TaskID);
        
        if (task == null) return task;
        task.Description = updateTask.Description;
        task.Title = updateTask.Title;

        _toDoDbContext.SaveChanges();

        return task;
    }
}