using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var updatedTodo = await _todoRepository.UpdateTodo(updatedTask);
        
        //Todo: Getting a bit complex should consider making an error messaging service
        
        if (updatedTodo != null) return new JsonResult(new { message = "Todo updated", updatedTodo });
        
        var result = new JsonResult(new { message = "ID not found" })
        {
            StatusCode = (int)HttpStatusCode.NotFound
        };
        return result;

    }
    
    [HttpDelete]
    [Route("api/[Controller]/DeleteTodo")]
    [ValidateId]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var deletedTodo = await _todoRepository.DeleteTodo(id);
        
        //Todo: Getting a bit complex should consider making an error messaging service
        if (deletedTodo != null) return new JsonResult(new
        {
            message = "Successfully deleted the following Todo", 
            deletedTodo
        });
        
        var result = new JsonResult(new { message = "ID not found" })
        {
            StatusCode = (int)HttpStatusCode.NotFound
        };
        return result;
    }

    //Todo: Temporary home for this, needs work
    [AttributeUsage(AttributeTargets.Method)]
    private class ValidateIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.ContainsKey("id"))
            {
                context.Result = new BadRequestObjectResult("id paramater is missing");
                return;
            }

            if (!int.TryParse(context.ActionArguments["id"].ToString(), out _))
            {
                context.Result = new BadRequestObjectResult("id paramater must be an integer");
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}