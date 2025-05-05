namespace FleetDepot.Types.Models;

public class ChassisId
{
    public string Series { get; set; } = default!;
    public uint Number { get; set; }

    public ChassisId(string series, uint number)
    {
        Series = series;
        Number = number;
    }

    public override string ToString() => $"{Series}-{Number}";
}
