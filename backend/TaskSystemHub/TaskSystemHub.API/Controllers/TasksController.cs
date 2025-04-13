using TaskSystemHub.Domain.Entities;
using TaskSystemHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TaskSystemHub.Shared.DTOs;
using Microsoft.Extensions.Logging;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IJiraService _jiraService;
    private readonly ILogger _logger;
    // ghi log
    public TaskController(ITaskRepository taskRepository, IJiraService jiraService, ILogger<TaskController> logger)
    {
        _taskRepository = taskRepository;
        _jiraService = jiraService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _taskRepository.GetAllTasksAsync();
        _logger.LogInformation("Get Task success");
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
    {
        await _taskRepository.AddTaskAsync(task);
        return CreatedAtAction(nameof(GetAllTasks), new { id = task.Id }, task);
    }

    [HttpPost("create-and-transition")]
    public async Task<IActionResult> CreateAndTransition([FromBody] CreateIssueRequestDto createDto)
    {
        var result = await _jiraService.CreateIssueAndTransitionAsync(createDto, "61");

        if (!result.IsIssueCreated)
        {
            return StatusCode(500, new
            {
                success = false,
                message = result.Message
            });
        }

        return Ok(new
        {
            success = true,
            message = result.Message,
            issue = new
            {
                result.IssueInfo?.id,
                result.IssueInfo?.key,
                result.IssueInfo?.self
            },
            transitioned = result.IsTransitioned
        });
    }

}
