using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public class NewTodoDto
{

    public string Title { get; set; }

    public string Description { get; set; }
}