using BeeProject.Filters;
using BeeProject.TransferModels;
using BeeProject.TransferModels.CreateRequests;
using BeeProject.TransferModels.UpdateRequests;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace BeeProject.Controllers;

public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [Route("/api/getTask")]
    public ResponseDto GetAllTask()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched every task.",
            ResponseData = _taskService.GetAllTasks()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/createTask")]
    public ResponseDto CreateTask([FromBody] CreateTaskRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a task.",
            ResponseData = _taskService.CreateTask(dto.HiveId, dto.Name, dto.Description!, dto.Done)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/updateTask")]
    public ResponseDto UpdateTask([FromBody] UpdateTaskRequestDto dto)
    {
        var task = new TaskQuery
        {
            Id = dto.Id,
            Hive_Id = dto.HiveId,
            Name = dto.Name,
            Description = dto.Description,
            Done = dto.Done
        };
        _taskService.UpdateTask(task);
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated task."
        };
    }

    //TODO: change to safe later
    [HttpDelete]
    [Route("/api/DeleteTask/{id:int}")]
    public ResponseDto DeleteTask([FromRoute] int id)
    {
        _taskService.DeleteTask(id);
        return new ResponseDto()
        {
            MessageToClient = "Successfully deleted task."
        };
    }
}