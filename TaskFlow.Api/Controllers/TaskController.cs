using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController:ControllerBase
{
    private readonly List<Models.Task> _tasks;

    public TaskController()
    {
        _tasks = new List<Models.Task>();
    }

    [HttpGet]
    public ActionResult<IEnumerable<Models.Task>> GetTasks()
    {
        return Ok(_tasks);
    }

    [HttpPost]
    public ActionResult<Models.Task> CreateTask(Models.Task task)
    {
        task.Id = _tasks.Count + 1;
        _tasks.Add(task);
        return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }
}
