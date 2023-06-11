using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos;

public class TodoDto
{
    [Required]
    public int? TaskID { get; set; }
    [StringLength(60), MinLength(3)]
    public string Title { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
}