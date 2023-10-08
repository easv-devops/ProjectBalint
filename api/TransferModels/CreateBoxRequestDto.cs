using System.ComponentModel.DataAnnotations;

namespace BoxFactory.TransferModels;

public class CreateBoxRequestDto
{
    public int Volume { get; set; }
    [MinLength(3)] public string Name { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
}