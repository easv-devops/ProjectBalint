using BeeProject.Filters;
using BeeProject.TransferModels;
using BeeProject.TransferModels.CreateRequests;
using BeeProject.TransferModels.UpdateRequests;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace BeeProject.Controllers;

public class HarvestController : ControllerBase
{
    private readonly HarvestService _harvestService;

    public HarvestController(HarvestService harvestService)
    {
        _harvestService = harvestService;
    }

    [HttpGet]
    [Route("/api/getHarvests")]
    public ResponseDto GetAllHarvests()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched all harvests.",
            ResponseData = _harvestService.GetAllHarvests()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/createHarvest")]
    public ResponseDto CreateHarvest([FromBody] CreateHarvestRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a harvest.",
            ResponseData =
                _harvestService.CreateHarvest(dto.HiveId, dto.Time, dto.HoneyAmount, dto.BeeswaxAmount, dto.Comment)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/updateHarvest")]
    public ResponseDto UpdateHarvest([FromBody] UpdateHarvestRequestDto dto)
    {
        var harvest = new HarvestQuery
        {
            Id = dto.Id,
            Hive_Id = dto.HiveId,
            Time = dto.Time,
            Honey_Amount = dto.HoneyAmount,
            Beeswax_Amount = dto.BeeswaxAmount,
            Comment = dto.Comment
        };
        _harvestService.UpdateHarvest(harvest);
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated harvest."
        };
    }

    //TODO: change to safe later
    [HttpDelete]
    [Route("/api/DeleteHarvest/{id:int}")]
    public ResponseDto DeleteHarvest([FromRoute] int id)
    {
        _harvestService.DeleteHarvest(id);
        return new ResponseDto()
        {
            MessageToClient = "Successfully deleted harvest."
        };
    }
}