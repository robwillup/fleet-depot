using AutoMapper;
using FleetDepot.Types.Dtos;
using FleetDepot.Types.Models;
using FleetDepot.Dal.Repositories;

namespace FleetDepot.Core.Services;

public class BusService(IMapper mapper, IVehicleRepository<Vehicle> repository) : VehicleService<BusDto, Vehicle>(mapper, repository), IVehicleService<BusDto>
{
    public override async Task<BusDto> AddAsync(BusDto dto)
    {
        dto.Type = nameof(Vehicle.VehicleType.Bus);
        dto.NumberOfPassengers = 42;
        return await base.AddAsync(dto);
    }

    public override async Task<IEnumerable<BusDto>> GetAllAsync()
    {
        var buses = await _repository.GetAllAsync(Vehicle.VehicleType.Bus);
        return buses.Select(_mapper.Map<BusDto>);
    }
}
