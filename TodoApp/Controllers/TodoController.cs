using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Interfaces;


namespace TodoApp.Controllers;

[Controller]
[Route("[Controller]")]
public class TodoController : ControllerBase
{
    private ILogger<TodoController> _logger;
    private IToDoRepository _todoRepository;
    public TodoController(ILogger<TodoController> logger, IToDoRepository todoRepository)
    {
        _todoRepository = todoRepository;
        _logger = logger;
    }

    [HttpPost]
    [Route("[Controller]/CreateNewTodo")]
    public void CreateNewTodo(string title, string description)
    {
        var todoTask = _todoRepository.CreateNewTodo(title, description);
    }

    [HttpGet]
    public IEnumerable<TodoTask> GetTasks()
    {
        var tasks = _todoRepository.GetAllTasks();

        return tasks;
    }
}