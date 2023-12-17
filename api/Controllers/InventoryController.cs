using BeeProject.Filters;
using BeeProject.TransferModels;
using BeeProject.TransferModels.CreateRequests;
using BeeProject.TransferModels.UpdateRequests;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace BeeProject.Controllers;

public class InventoryController : ControllerBase
{
    private readonly InventoryService _inventoryService;

    public InventoryController(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    [Route("/api/getInventory")]
    public ResponseDto GetAllInventory()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched every inventory.",
            ResponseData = _inventoryService.GetAllInventoryItems()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/createInventory")]
    public ResponseDto CreateInventory([FromBody] CreateInventoryRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a inventory.",
            ResponseData = _inventoryService.CreateInventoryItem(dto.FieldId, dto.Name, dto.Description, dto.Amount)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/updateInventory")]
    public ResponseDto UpdateInventory([FromBody] UpdateInventoryRequestDto dto)
    {
        var inventory = new InventoryQuery
        {
            Id = dto.Id,
            Field_Id = dto.FieldId,
            Name = dto.Name,
            Description = dto.Description,
            Amount = dto.Amount
        };
        _inventoryService.UpdateInventoryItem(inventory);
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated inventory."
        };
    }

    //TODO: change to safe later
    [HttpDelete]
    [Route("/api/DeleteInventory/{id:int}")]
    public ResponseDto DeleteInventory([FromRoute] int id)
    {
        _inventoryService.DeleteInventoryItem(id);
        return new ResponseDto()
        {
            MessageToClient = "Successfully deleted inventory."
        };
    }
}