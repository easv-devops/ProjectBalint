using BeeProject.Filters;
using BeeProject.TransferModels;
using BeeProject.TransferModels.CreateRequests;
using BeeProject.TransferModels.UpdateRequests;
using infrastructure.QueryModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace BeeProject.Controllers;

public class FieldController : ControllerBase
{
    private readonly FieldService _fieldService;

    public FieldController(FieldService fieldService)
    {
        _fieldService = fieldService;
    }

    [HttpGet]
    [Route("/api/getFields")]
    public ResponseDto GetAllFields()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched all fields.",
            ResponseData = _fieldService.GetAllFields()
        };
    }
    [HttpGet]
    [Route("/api/getFieldsForAccount/{id:int}")]
    public ResponseDto GetFieldsForAccount([FromRoute] int id)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched all fields for account.",
            ResponseData = _fieldService.GetFieldsForAccount(id)
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/createField")]
    public ResponseDto CreateField([FromBody] CreateFieldRequestDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a field.",
            ResponseData = _fieldService.CreateField(dto.FieldName, dto.FieldLocation)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/updateField")]
    public ResponseDto UpdateField([FromBody] UpdateFieldRequestDto dto)
    {
        var field = new FieldQuery
        {
            Id = dto.FieldId,
            Name = dto.FieldName,
            Location = dto.FieldLocation
        };
        _fieldService.UpdateField(field);
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated field."
        };
    }

    //TODO: change to safe later
    [HttpDelete]
    [Route("/api/DeleteField/{id:int}")]
    public ResponseDto DeleteField([FromRoute] int id)
    {
        _fieldService.DeleteField(id);
        return new ResponseDto()
        {
            MessageToClient = "Successfully deleted field."
        };
    }
    
    [HttpPost]
    [ValidateModel]
    [Route("/api/ConnectFieldAndAccount")]
    public ResponseDto ConnectFieldAndAccount([FromBody] FieldAndAccountDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully connected field to account.",
            ResponseData = _fieldService.ConnectFieldAndAccount(dto.AccountId, dto.FieldId)
        };
    }
    [HttpDelete]
    [ValidateModel]
    [Route("/api/DisconnectFieldAndAccount")]
    public ResponseDto DisconnectFieldAndAccount([FromBody] FieldAndAccountDto dto)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully disconnected field from account.",
            ResponseData = _fieldService.DisconnectFieldAndAccount(dto.AccountId, dto.FieldId)
        };
    }
}