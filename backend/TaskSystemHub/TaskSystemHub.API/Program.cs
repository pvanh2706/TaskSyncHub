// using TaskSystemHub.Application.Services;
// using TaskSystemHub.Infrastructure.Jira;
using TaskSystemHub.Application.Interfaces;
using TaskSystemHub.Domain;
using TaskSystemHub.Persistence;
using TaskSystemHub.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container
// builder.Services.AddScoped<JiraApiClient>();
// builder.Services.AddScoped<JiraService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
// builder.Services.AddScoped<ITaskService, TaskService>(); 

builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();
app.Run();

