using System.ComponentModel.DataAnnotations.Schema;

namespace FleetDepot.Types.Models;

public class Vehicle
{
    public string Series { get; set; } = default!;
    public uint Number { get; set; }
    public string Color { get; set; } = default!;
    public uint NumberOfPassengers { get; set; }
    public VehicleType Type { get; set; } = default!;

    public override bool Equals(object? obj)
    {
        if (obj is Vehicle vehicle)
        {
            return Series == vehicle.Series &&
                Number == vehicle.Number &&
                Color == vehicle.Color &&
                NumberOfPassengers == vehicle.NumberOfPassengers &&
                Type == vehicle.Type;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Series, Number, Color, NumberOfPassengers, Type);
    }


    [NotMapped]
    public ChassisId Id
    {
        get => new(Series, Number);
        set
        {
            Series = value.Series;
            Number = value.Number;
        }
    }

    public enum VehicleType
    {
        Bus,
        Car,
        Truck
    }
}
