using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data;


public class Task
{
    [Key]
    public int TaskID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
public class ToDoDbContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }

    public ToDoDbContext()
    {
    }
}