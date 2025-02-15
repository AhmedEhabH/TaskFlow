using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Controllers;
using TaskFlow.Api.Models;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace TaskFlow.Tests;

public class TaskControllerTests
{
    private readonly TaskController _controller;

    public TaskControllerTests()
    {
        var loggerMock = new Mock<ILogger<TaskController>>();
        _controller = new TaskController(loggerMock.Object);
    }

    [Fact]
    public void GetTasks_ShouldReturnEmptyList_WhenNoTasksExist()
    {
        var result = _controller.GetTasks().Result as OkObjectResult;
        var tasks = result?.Value as List<Api.Models.Task>;

        tasks.Should().BeEmpty();
    }

    [Fact]
    public void CreateTask_ShouldReturnCreatedTask()
    {
        var newTask = new Api.Models.Task { Title = "Test Task", IsCompleted = false };
        var result = _controller.CreateTask(newTask).Result as CreatedAtActionResult;

        result.Should().NotBeNull();
        result?.Value.Should().BeEquivalentTo(newTask, options => options.Excluding(t => t.Id));
    }
}
