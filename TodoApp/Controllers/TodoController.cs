using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Dtos;
using TodoApp.Interfaces;


namespace TodoApp.Controllers;

[Controller]
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
    
    public async Task<IActionResult> CreateNewTodo([FromBody] NewTodoDto newTodo)
    {
        var todoTask = _todoRepository.CreateNewTodo(newTodo.Title, newTodo.Description);

        return todoTask == null ? Problem() : Created("TodoCreated", todoTask);
    }

    [HttpGet]
    [Route("[Controller]/GetTasks")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = _todoRepository.GetAllTasks();
        
        return tasks == null ? Problem() : Accepted("Success", tasks);
    }

    [HttpPatch]
    [Route("[Controller]/UpdateTodo")]
    public async Task<IActionResult> UpdateTodo([FromBody] TodoTask updatedTask)
    {
        var updatedTodo = _todoRepository.UpdateTodo(updatedTask.TaskID, updatedTask.Title, updatedTask.Description);

        return updatedTodo == null ? Problem() : 
            new JsonResult(new
            {
                message = "Todo updated", updatedTodo
            });
    }

    [HttpDelete]
    [Route("[Controller]/DeleteTodo")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var deletedTodo = _todoRepository.DeleteTodo(id);
        
        

        return deletedTodo == null ? Problem() : 
            new JsonResult(new
            {
                message = "Successfully deleted the following Todo", 
                deletedTodo
            });
    }

}