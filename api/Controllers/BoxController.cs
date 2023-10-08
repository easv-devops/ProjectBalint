using System.ComponentModel.DataAnnotations;
using BoxFactory.Filters;
using BoxFactory.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace BoxFactory.Controllers;

public class BoxController : ControllerBase
{
    private readonly ILogger<BoxController> _logger;
    private readonly BoxService _boxService;

    public BoxController(ILogger<BoxController> logger, BoxService boxService)
    {
        _logger = logger;
        _boxService = boxService;
    }

    [HttpGet]
    [Route("/api/boxes")]
    public ResponseDto GetAllBoxes()
    {
        HttpContext.Response.StatusCode = StatusCodes.Status302Found;
        return new ResponseDto()
        {
            MessageToClient = "Successfully fetched all boxes.",
            ResponseData = _boxService.GetBoxesForFeed()
        };
    }

    [HttpGet]
    [Route("/api/boxes/{id}")]
    public ResponseDto GetBoxById()
    {
        HttpContext.Response.StatusCode = StatusCodes.Status302Found;
        return new ResponseDto()
        {
            MessageToClient = "Successfully found box with given id.",
            ResponseData = _boxService.GetBoxesForFeed()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/api/box")]
    public ResponseDto CreateBox([FromBody] CreateBoxRequestDto dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a box.",
            ResponseData = _boxService.CreateBox(dto.Volume, dto.Name, dto.Color, dto.Description)
        };
    }

    [HttpPut]
    [ValidateModel]
    [Route("/api/box/{boxId}")]
    public ResponseDto UpdateBox([FromRoute]int boxId, [FromBody] UpdateBoxRequestDto dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated book.",
            ResponseData = _boxService.UpdateBox(dto.Id, dto.Volume, dto.Name, dto.Color, dto.Description)
        };
    }

    [HttpDelete]
    [Route("/api/DeleteBox")]
    public object DeleteBox()
    {
        return null;
    }
}



public record Box
{
    public int Id { get; set; }
    public int Volume { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return $"{{ Name = {Name} }}";
    }
}



