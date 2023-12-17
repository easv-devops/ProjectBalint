using BeeProject.Filters;
using BeeProject.TransferModels;
using BeeProject.TransferModels.CreateRequests;
using BeeProject.TransferModels.UpdateRequests;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace BeeProject.Controllers;
//TODO: convert dates and times, probably easier to handle as a string
public class HiveController : ControllerBase
{
    private readonly HiveService _hiveService;

    public HiveController(HiveService hiveService)
    {
        _hiveService = hiveService;
    }

    [HttpGet]
    [Route("/api/getHives")]
    public ResponseDto GetAllHives()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched all hives.",
            ResponseData = _hiveService.GetAllHives()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/createHive")]
    public ResponseDto CreateHive([FromBody] CreateHiveRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a hive.",
            ResponseData = _hiveService.CreateHive(dto.FieldId, dto.Name, dto.Location, dto.PlacementDate,
                dto.LastCheck, dto.ReadyToHarvest, dto.Color, dto.Comment!, dto.BeeId)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/updateHive")]
    public ResponseDto UpdateHive([FromBody] UpdateHiveRequestDto dto)
    {
        var hive = new HiveQuery
        {
            Id = dto.Id,
            Field_Id = dto.FieldId,
            Name = dto.Name,
            Location = dto.Location,
            Placement = dto.PlacementDate,
            Last_Check = dto.LastCheck,
            Ready = dto.ReadyToHarvest,
            Color = dto.Color,
            Bee_Type = dto.BeeId,
            Comment = dto.Comment
        };
        _hiveService.UpdateHive(hive);
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated hive."
        };
    }

    //TODO: change to safe later
    [HttpDelete]
    [Route("/api/DeleteHive/{id:int}")]
    public ResponseDto DeleteHive([FromRoute] int id)
    {
        _hiveService.DeleteHive(id);
        return new ResponseDto()
        {
            MessageToClient = "Successfully deleted hive."
        };
    }
}