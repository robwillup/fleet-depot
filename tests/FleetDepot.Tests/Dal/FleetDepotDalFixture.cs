using FleetDepot.Dal;
using FleetDepot.Dal.Repositories;
using FleetDepot.Types.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FleetDepot.Tests.Dal;

public class FleetDepotDalFixture
{
    public ServiceProvider Services { get; }

    public FleetDepotDalFixture()
    {
        ServiceCollection services = new();

        services.AddDbContext<FleetDepotDb>();
        services.AddTransient<IVehicleRepository<Vehicle>, VehicleRepository<Vehicle>>();

        Services = services.BuildServiceProvider();
    }
}
