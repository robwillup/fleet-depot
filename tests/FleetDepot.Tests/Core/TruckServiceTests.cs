﻿using FleetDepot.Core.Services;
using FleetDepot.Dal.Repositories;
using FleetDepot.Types.Dtos;
using FleetDepot.Types.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FleetDepot.Tests.Core;

public class TruckServiceTests(FleetDepotCoreFixture fixture) : IClassFixture<FleetDepotCoreFixture>
{
    private const uint NUMBER_OF_TRUCK_PASSENGERS = 1;
    private readonly IVehicleService<TruckDto> _service = fixture.Services.GetRequiredService<IVehicleService<TruckDto>>();
    private readonly IVehicleRepository<Vehicle> _repository = fixture.Services.GetRequiredService<IVehicleRepository<Vehicle>>();

    [Fact]
    public async Task AddAsync_NumberOfPassengersNotProvided_EnsuresExpectedNumber()
    {
        // Arrange
        TruckDto dto = new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "green" };

        try
        {
            // Act
            TruckDto actual = await _service.AddAsync(dto);

            // Assert
            Assert.Equal(NUMBER_OF_TRUCK_PASSENGERS, actual.NumberOfPassengers);
        }
        finally
        {
            await _service.DeleteAsync(dto.Series, dto.Number);
        }
    }

    [Fact]
    public async Task GetAllAsync_WithDifferentVehicleTypes_ReturnsOnlyCars()
    {
        // Arrange
        var vehicles = new List<Vehicle>()
        {
            { new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "green", NumberOfPassengers = 4, Type = Vehicle.VehicleType.Car } },
            { new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "blue", NumberOfPassengers = 42, Type = Vehicle.VehicleType.Bus } },
            { new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "white", NumberOfPassengers = 42, Type = Vehicle.VehicleType.Truck } },
            { new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "black", NumberOfPassengers = 1, Type = Vehicle.VehicleType.Truck } }
        };

        try
        {
            // Act
            foreach (var vehicle in vehicles)
            {
                await _repository.AddAsync(vehicle);
            }

            IEnumerable<VehicleDto> actual = await _service.GetAllAsync();

            // Assert
            foreach (var vehicle in actual)
            {
                Assert.IsType<TruckDto>(vehicle);
            }
        }
        finally
        {
            foreach (var vehicle in vehicles)
            {
                await _repository.DeleteAsync(vehicle);
            }
        }
    }
}
