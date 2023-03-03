using TodoApp.Data;
using TodoApp.Dtos;

namespace TodoApp.Interfaces;

public interface IToDoRepository
{
    public Task<TodoTask> CreateNewTodo(NewTodoDto newTodoDto);
    public IEnumerable<TodoTask> GetAllTasks();

    public TodoTask DeleteTodo(int id);
    public Task<TodoTask?> UpdateTodo(TodoTask updateTask);
}