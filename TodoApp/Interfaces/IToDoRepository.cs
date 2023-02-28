using TodoApp.Data;

namespace TodoApp.Interfaces;

public interface IToDoRepository
{
    public Task<TodoTask> CreateNewTodo(string title, string description);
    public IEnumerable<TodoTask> GetAllTasks();
}