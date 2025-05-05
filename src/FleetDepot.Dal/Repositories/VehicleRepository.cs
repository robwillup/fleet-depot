using FleetDepot.Types.Models;
using Microsoft.EntityFrameworkCore;
using static FleetDepot.Types.Models.Vehicle;

namespace FleetDepot.Dal.Repositories;

public class VehicleRepository<TEntity>(FleetDepotDb context) : IVehicleRepository<TEntity>
    where TEntity : Vehicle
{
    protected readonly FleetDepotDb _context = context;

    public async Task<IEnumerable<TEntity>> GetAllAsync(VehicleType? vehicleType = null)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (vehicleType.HasValue)
        {
            query = query.Where(v => v.Type == vehicleType);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(TEntity vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> GetAsync(ChassisId id)
    {
        TEntity? vehicle = await _context.Set<TEntity>().FirstOrDefaultAsync(v => v.Series == id.Series && v.Number == id.Number);
        return vehicle!;
    }

    public async Task UpdateAsync(TEntity vehicle)
    {
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity vehicle)
    {
        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();
    }
}
