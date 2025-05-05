using FleetDepot.Core.Mappers;
using FleetDepot.Core.Services;
using FleetDepot.Dal;
using FleetDepot.Dal.Repositories;
using FleetDepot.Types.Dtos;
using FleetDepot.Types.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FleetDepot.Tests.Core;

public class FleetDepotCoreFixture
{
    public ServiceProvider Services { get; }

    public FleetDepotCoreFixture()
    {
        ServiceCollection services = new();

        services.AddDbContext<FleetDepotDb>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient<IVehicleRepository<Vehicle>, VehicleRepository<Vehicle>>();
        services.AddTransient<IVehicleService<CarDto>, CarService>();
        services.AddTransient<IVehicleService<BusDto>, BusService>();
        services.AddTransient<IVehicleService<TruckDto>, TruckService>();
        services.AddTransient<IVehicleService<VehicleDto>, VehicleService<VehicleDto, Vehicle>>();

        Services = services.BuildServiceProvider();
    }
}
