using System.ComponentModel.DataAnnotations;

namespace BeeProject.TransferModels.UpdateRequests;

public class UpdateHiveRequestDto
{
    public int Id { get; set; }
    public int FieldId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$",
        ErrorMessage = "Invalid timestamp format. Use dd-MM-yyyy.")]
    public string PlacementDate { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$",
        ErrorMessage = "Invalid timestamp format. Use dd-MM-yyyy HH:mm:ss.")]
    public string LastCheck { get; set; }

    public bool ReadyToHarvest { get; set; }
    public string Color { get; set; }
    public int BeeId { get; set; }
    public string? Comment { get; set; }
}