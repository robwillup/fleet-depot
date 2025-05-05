using System.Text.Json.Serialization;

namespace FleetDepot.Types.Dtos;

public class VehicleDto()
{
    [JsonPropertyName("series")]
    public string Series { get; set; }
    [JsonPropertyName("number")]
    public uint Number { get; set; }
    [JsonPropertyName("numberOfPassengers")]
    public uint NumberOfPassengers { get; set; }
    [JsonPropertyName("color")]
    public string Color { get; set; }
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}
