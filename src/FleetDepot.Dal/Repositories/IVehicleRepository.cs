using FleetDepot.Types.Models;
using static FleetDepot.Types.Models.Vehicle;

namespace FleetDepot.Dal.Repositories;

public interface IVehicleRepository<TEntity>
{
    public Task AddAsync(TEntity vehicle);
    public Task UpdateAsync(TEntity vehicle);
    public Task<IEnumerable<TEntity>> GetAllAsync(VehicleType? vehicleType = null);
    public Task<TEntity> GetAsync(ChassisId id);
    public Task DeleteAsync(TEntity vehicle);
}
