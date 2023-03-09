using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public class TodoDto
{
    [Required]
    public int? TaskID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}