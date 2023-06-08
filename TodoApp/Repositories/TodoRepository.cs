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

    public async Task<TodoTask?> DeleteTodo(int id)
    {
        var task = _toDoDbContext.Tasks.FirstOrDefault(t => t.TaskID == id);

        if (task == null) return null;
        
        _toDoDbContext.Tasks.Remove(task);
        await _toDoDbContext.SaveChangesAsync();

        return task;
    }

    
    public async Task<TodoTask?> UpdateTodo(TodoDto updateTask)
    {
        var task = await _toDoDbContext.Tasks.FirstOrDefaultAsync(t => t.TaskID == updateTask.TaskID);
        
        if (task == null) return task;
        task.Description = updateTask.Description;
        task.Title = updateTask.Title;

        await _toDoDbContext.SaveChangesAsync();

        return task;
    }
}