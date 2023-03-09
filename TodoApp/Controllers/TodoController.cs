using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
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
    [Route("api/[Controller]/CreateNewTodo")]
    
    public async Task<IActionResult> CreateNewTodo([FromBody] NewTodoDto newTodo)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var todoTask = await _todoRepository.CreateNewTodo(newTodo);
        if (todoTask != null) return Created("TodoCreated", todoTask);
        
        _logger.LogError("todoTask failed to create");
        return Problem("Todo failed to create");
    }

    [HttpGet]
    [Route("api/[Controller]/GetTasks")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = _todoRepository.GetAllTasks();
        return tasks == null ? Problem() : Accepted("Success", tasks);
    }

    [HttpPatch]
    [Route("api/[Controller]/UpdateTodo")]
    public async Task<IActionResult> UpdateTodo([FromBody] TodoDto updatedTask)
    {
        if (ModelState.IsValid)
        {
            var updatedTodo = _todoRepository.UpdateTodo(updatedTask);

            return updatedTodo == null
                ? Problem()
                : new JsonResult(new
                {
                    message = "Todo updated", updatedTodo
                });
        }

        return BadRequest(ModelState);
    }

    [HttpDelete]
    [Route("api/[Controller]/DeleteTodo")]
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