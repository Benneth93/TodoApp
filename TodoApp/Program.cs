using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TodoApp.Data;
using TodoApp.Interfaces;
using TodoApp.Repositlries;
using TodoApp.Services;

var builder = WebApplication.CreateBuilder(args);

SettingsRetrievalService.SettingsRetrievalServiceConfigure(builder.Configuration);
var connectionString = builder.Configuration.GetConnectionString("TododbConnectionString") ??
                       throw new InvalidOperationException("Connection stirng 'TododbConnectionString' not found");

builder.Services.AddDbContextPool<ToDoDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IToDoRepository, TodoRepository>();

var db = builder.Services.BuildServiceProvider().GetRequiredService<ToDoDbContext>();
db.Database.Migrate();
         
         // Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("DevelopmentDocker"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();