using BeeProject.Filters;
using BeeProject.TransferModels;
using BeeProject.TransferModels.CreateRequests;
using BeeProject.TransferModels.UpdateRequests;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace BeeProject.Controllers;

public class HoneyController : ControllerBase
{
    private readonly HoneyService _honeyService;

    public HoneyController(HoneyService honeyService)
    {
        _honeyService = honeyService;
    }

    [HttpGet]
    [Route("/api/getHoney")]
    public ResponseDto GetAllHoneys()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched every honey.",
            ResponseData = _honeyService.GetAllHoney()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/createHoney")]
    public ResponseDto CreateHoney([FromBody] CreateHoneyRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a honey.",
            ResponseData = _honeyService.CreateHoney(dto.Harvest, dto.Name, dto.Liquid, dto.Flowers, dto.Moisture)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/updateHoney")]
    public ResponseDto UpdateHoney([FromBody] UpdateHoneyRequestDto dto)
    {
        var honey = new HoneyQuery
        {
            Id = dto.Id,
            Name = dto.Name,
            Liquid = dto.Liquid,
            Harvest_id = dto.Harvest,
            Moisture = dto.Moisture,
            Flowers = dto.Flowers,
        };
        _honeyService.UpdateHoney(honey);
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated honey."
        };
    }

    //TODO: change to safe later
    [HttpDelete]
    [Route("/api/DeleteHoney/{id:int}")]
    public ResponseDto DeleteHoney([FromRoute] int id)
    {
        _honeyService.DeleteHoney(id);
        return new ResponseDto()
        {
            MessageToClient = "Successfully deleted honey."
        };
    }
}