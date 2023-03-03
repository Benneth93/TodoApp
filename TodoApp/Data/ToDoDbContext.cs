using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data;


public class TodoTask
{
    [Key]
    public int TaskID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
public class ToDoDbContext : DbContext
{
    public DbSet<TodoTask> Tasks { get; set; }

    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
    {
    }
}