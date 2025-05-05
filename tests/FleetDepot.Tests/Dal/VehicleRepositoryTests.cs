using FleetDepot.Dal.Repositories;
using FleetDepot.Types.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FleetDepot.Tests.Dal;

public class VehicleRepositoryTests(FleetDepotDalFixture fixture) : IClassFixture<FleetDepotDalFixture>
{
    private readonly IVehicleRepository<Vehicle> _vehicleRepository = fixture.Services.GetRequiredService<IVehicleRepository<Vehicle>>();

    [Fact]
    public async Task AddAsync_Car_Succeeds()
    {
        // Arrange
        string series = Guid.NewGuid().ToString();
        Vehicle expected = new() { Series = series, Number = 2, Color = "green", NumberOfPassengers = 4, Type = Vehicle.VehicleType.Car };

        try
        {
            // Act
            await _vehicleRepository.AddAsync(expected);

            // Assert
            Vehicle actual = await _vehicleRepository.GetAsync(new ChassisId(series, 2));
            Assert.True(actual.Equals(expected));
        }
        finally
        {
            await _vehicleRepository.DeleteAsync(expected);
        }
    }

    [Fact]
    public async Task GetAllAsync_OnlyTrucks_Succeeds()
    {
        // Arrange
        var vehicles = new List<Vehicle>()
        {
            { new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "green", NumberOfPassengers = 4, Type = Vehicle.VehicleType.Car } },
            { new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "blue", NumberOfPassengers = 42, Type = Vehicle.VehicleType.Bus } },
            { new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "black", NumberOfPassengers = 1, Type = Vehicle.VehicleType.Truck } }
        };

        try
        {
            foreach (var vehicle in vehicles)
            {
                await _vehicleRepository.AddAsync(vehicle);
            }

            // Act
            IEnumerable<Vehicle> actualVehicles = await _vehicleRepository.GetAllAsync(Vehicle.VehicleType.Truck);

            // Assert
            foreach (var vehicle in actualVehicles)
            {
                Assert.Equal(Vehicle.VehicleType.Truck, vehicle.Type);
            }
        }
        finally
        {
            foreach (var vehicle in vehicles)
            {
                await _vehicleRepository.DeleteAsync(vehicle);
            }
        }
    }

    [Fact]
    public async Task GetAsync_SameSeries_GetsOne()
    {
        // Arrange
        string truckSeries = Guid.NewGuid().ToString();
        var vehicles = new List<Vehicle>()
        {
            { new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "green", NumberOfPassengers = 4, Type = Vehicle.VehicleType.Car } },
            { new() { Series = Guid.NewGuid().ToString(), Number = 2, Color = "blue", NumberOfPassengers = 42, Type = Vehicle.VehicleType.Bus } },
            { new() { Series = truckSeries, Number = 3, Color = "black", NumberOfPassengers = 1, Type = Vehicle.VehicleType.Truck } }
        };

        try
        {
            foreach (var vehicle in vehicles)
            {
                await _vehicleRepository.AddAsync(vehicle);
            }

            // Act
            Vehicle actual = await _vehicleRepository.GetAsync(new ChassisId(truckSeries, 3));

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Equals(vehicles.Last()));
        }
        finally
        {
            foreach (var vehicle in vehicles)
            {
                await _vehicleRepository.DeleteAsync(vehicle);
            }
        }
    }

    [Fact]
    public async Task UpdateAsync_Bus_ChangesColor()
    {
        // Arrange
        Vehicle vehicle = new() { Series = Guid.NewGuid().ToString(), Number = 2, Color = "blue", NumberOfPassengers = 42, Type = Vehicle.VehicleType.Bus };

        try
        {
            await _vehicleRepository.AddAsync(vehicle);

            vehicle.Color = "white";

            // Act
            await _vehicleRepository.UpdateAsync(vehicle);

            // Assert
            Assert.NotEqual("blue", vehicle.Color);
        }
        finally
        {
            await _vehicleRepository.DeleteAsync(vehicle);
        }
    }

    [Fact]
    public async Task DeleteAsync_DeleteCar_Succeeds()
    {
        // Arrange
        string series = Guid.NewGuid().ToString();
        Vehicle vehicle = new() { Series = series, Number = 1, Color = "green"};
        await _vehicleRepository.AddAsync(vehicle);

        // Act
        await _vehicleRepository.DeleteAsync(vehicle);

        // Assert
        Assert.Null(await _vehicleRepository.GetAsync(new(series, 1)));
    }

    [Fact]
    public async Task AddAsync_Duplicate_Fails()
    {
        // Arrange
        string series = Guid.NewGuid().ToString();
        Vehicle expected = new() { Series = series, Number = 2, Color = "green", NumberOfPassengers = 4, Type = Vehicle.VehicleType.Car };

        try
        {
            // Act
            await _vehicleRepository.AddAsync(expected);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _vehicleRepository.AddAsync(expected));
        }
        finally
        {
            await _vehicleRepository.DeleteAsync(expected);
        }
    }
}
