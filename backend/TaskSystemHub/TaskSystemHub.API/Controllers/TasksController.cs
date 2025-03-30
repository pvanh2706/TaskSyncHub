using TaskSystemHub.Domain.Entities;
using TaskSystemHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _taskRepository.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
    {
        await _taskRepository.AddTaskAsync(task);
        return CreatedAtAction(nameof(GetAllTasks), new { id = task.Id }, task);
    }
}