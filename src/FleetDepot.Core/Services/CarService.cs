using AutoMapper;
using FleetDepot.Types.Dtos;
using FleetDepot.Types.Models;
using FleetDepot.Dal.Repositories;

namespace FleetDepot.Core.Services;

public class CarService(IMapper mapper, IVehicleRepository<Vehicle> repository) : VehicleService<CarDto, Vehicle>(mapper, repository), IVehicleService<CarDto>
{
    public override async Task<CarDto> AddAsync(CarDto dto)
    {
        dto.Type = nameof(Vehicle.VehicleType.Car);
        dto.NumberOfPassengers = 4;
        return await base.AddAsync(dto);
    }

    public override async Task<IEnumerable<CarDto>> GetAllAsync()
    {
        var cars = await _repository.GetAllAsync(Vehicle.VehicleType.Car);
        return cars.Select(_mapper.Map<CarDto>);
    }
}
