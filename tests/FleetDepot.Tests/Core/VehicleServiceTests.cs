using FleetDepot.Core.Services;
using FleetDepot.Types.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace FleetDepot.Tests.Core;

public class VehicleServiceTests(FleetDepotCoreFixture fixture) : IClassFixture<FleetDepotCoreFixture>
{
    private readonly IVehicleService<TruckDto> _truckService = fixture.Services.GetRequiredService<IVehicleService<TruckDto>>();

    [Fact]
    public async Task DeleteAsync_WithOneTruckAdded_EnsuresItIsDeleted()
    {
        // Arrange
        TruckDto dto = new() { Series = Guid.NewGuid().ToString(), Number = 1, Color = "green" };
        _ = await _truckService.AddAsync(dto);

        // Act
        await _truckService.DeleteAsync(dto.Series, dto.Number);

        // Assert
        TruckDto actual = await _truckService.GetByChassisIdAsync(dto.Series, dto.Number);
        Assert.Null(actual);
    }

    [Fact]
    public async Task UpdateAsync_WithColorGreen_ChangesToWhite()
    {
        // Arrange
        string expected = "white";
        TruckDto dto = new() { Series = Guid.NewGuid().ToString(), Number = 2, Color = "green" };
        await _truckService.AddAsync(dto);
        dto.Color = expected;

        try
        {
            // Act
            await _truckService.UpdateAsync(dto.Series, dto.Number, dto);

            string actual = (await _truckService.GetByChassisIdAsync(dto.Series, dto.Number)).Color;

            // Assert
            Assert.Equal(expected, actual);
        }
        finally
        {
            await _truckService.DeleteAsync(dto.Series, dto.Number);
        }
    }
}
