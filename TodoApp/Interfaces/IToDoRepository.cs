using TodoApp.Data;

namespace TodoApp.Interfaces;

public interface IToDoRepository
{
    public TodoTask CreateNewTodo(string title, string description);
    public IEnumerable<TodoTask> GetAllTasks();

    public TodoTask DeleteTodo(int id);
    public TodoTask UpdateTodo(int id, string title, string description);
}