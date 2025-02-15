using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TaskController:ControllerBase
{
    private readonly List<Models.Task> _tasks;
    private readonly ILogger<TaskController> _logger;

    public TaskController(ILogger<TaskController> logger)
    {
        _tasks = new List<Models.Task>();
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Models.Task>> GetTasks()
    {
        return Ok(_tasks);
    }

    [HttpPost("create-task")]
    public ActionResult<Models.Task> CreateTask(Models.Task task)
    {
        //_logger.LogInformation(task.Title);
        
        if (task == null)
            return BadRequest("Invalid task data.");
        task.Id = _tasks.Count + 1;
        _tasks.Add(task);
        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }
}
