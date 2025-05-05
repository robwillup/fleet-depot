namespace FleetDepot.Core.Services;

public interface IVehicleService<TDto>
{
    public Task<IEnumerable<TDto>> GetAllAsync();
    public Task<TDto> GetByChassisIdAsync(string series, uint number);
    public Task<TDto> AddAsync(TDto vehicleDto);
    public Task UpdateAsync(string series, uint number, TDto vehicleDto);
    public Task DeleteAsync(string series, uint number);
}
