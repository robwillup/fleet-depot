using AutoMapper;
using FleetDepot.Types.Dtos;
using FleetDepot.Types.Models;
using FleetDepot.Dal.Repositories;

namespace FleetDepot.Core.Services;

public class TruckService(IMapper mapper, IVehicleRepository<Vehicle> repository) : VehicleService<TruckDto, Vehicle>(mapper, repository), IVehicleService<TruckDto>
{
    public override async Task<TruckDto> AddAsync(TruckDto dto)
    {
        dto.Type = nameof(Vehicle.VehicleType.Truck);
        dto.NumberOfPassengers = 1;
        return await base.AddAsync(dto);
    }

    public override async Task<IEnumerable<TruckDto>> GetAllAsync()
    {
        var trucks = await _repository.GetAllAsync(Vehicle.VehicleType.Truck);
        return trucks.Select(_mapper.Map<TruckDto>);
    }
}
