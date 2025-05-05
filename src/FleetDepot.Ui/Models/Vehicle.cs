using System.ComponentModel.DataAnnotations;

namespace FleetDepot.Ui.Models;

public class Vehicle
{
    public string Series { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Number must be a positive value.")]
    public int Number { get; set; }
    public string Color { get; set; }
    public int NumberOfPassengers { get; set; }
    public string Type { get; set; }
}
