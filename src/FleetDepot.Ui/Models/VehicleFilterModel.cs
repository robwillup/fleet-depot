using System.ComponentModel.DataAnnotations;

namespace FleetDepot.Ui.Models;

public class VehicleFilterModel
{
    [Required]
    public string? Series { get; set; }
    [Required]
    public string? Number { get; set; }
}
