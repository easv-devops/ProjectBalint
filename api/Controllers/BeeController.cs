using System.Security.Cryptography;
using BeeProject.Filters;
using BeeProject.TransferModels;
using BeeProject.TransferModels.CreateRequests;
using BeeProject.TransferModels.UpdateRequests;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace BeeProject.Controllers;

public class BeeController : ControllerBase
{
    private readonly BeeService _beeService;

    public BeeController(BeeService beeService)
    {
        _beeService = beeService;
    }

    [HttpGet]
    [Route("/api/getBees")]
    public ResponseDto GetAllBees()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched all bees.",
            ResponseData = _beeService.GetAllBees()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/createBee")]
    public ResponseDto CreateBee([FromBody] CreateBeeRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a bee.",
            ResponseData = _beeService.CreateBee(dto.Name, dto.Description, dto.Comment!)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/updateBee")]
    public ResponseDto UpdateBee([FromBody] UpdateBeeRequestDto dto)
    {
        var bee = new BeeQuery
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Comment = dto.Comment
                
        };
        _beeService.UpdateBee(bee);
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated bee."
        };
    }

    //TODO: change to safe later
    [HttpDelete]
    [Route("/api/DeleteBee/{id:int}")]
    public ResponseDto DeleteBee([FromRoute] int id)
    {
        _beeService.DeleteBee(id);
        return new ResponseDto()
        {
            MessageToClient = "Successfully deleted bee."
        };
    }
}