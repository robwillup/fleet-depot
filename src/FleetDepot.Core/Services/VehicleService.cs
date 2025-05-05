using AutoMapper;
using FleetDepot.Types.Dtos;
using FleetDepot.Types.Exceptions;
using FleetDepot.Types.Models;
using FleetDepot.Dal.Repositories;

namespace FleetDepot.Core.Services;

public class VehicleService<TDto, TEntity>(IMapper mapper, IVehicleRepository<TEntity> repository) : IVehicleService<TDto>
    where TEntity : Vehicle, new()
    where TDto : VehicleDto
{
    protected readonly IMapper _mapper = mapper;
    protected readonly IVehicleRepository<TEntity> _repository = repository;

    public virtual async Task<TDto> AddAsync(TDto dto)
    {
        try
        {
            await _repository.AddAsync(_mapper.Map<TEntity>(dto));
            return dto;
        }
        catch (ArgumentException ex) when (ex.Message.Contains("same key"))
        {
            throw new DuplicateChassisIdException("A vehicle with the same chassis ID already exists.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("already being tracked"))
        {
            throw new DuplicateChassisIdException("A vehicle with the same chassis ID already exists.", ex);
        }
    }

    public async Task DeleteAsync(string series, uint number)
    {
        TEntity entity = await _repository.GetAsync(new(series, number));
        await _repository.DeleteAsync(entity);
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var vehicles = await _repository.GetAllAsync();
        return vehicles.Select(_mapper.Map<TDto>);
    }

    public async Task<TDto> GetByChassisIdAsync(string series, uint number)
    {
        return _mapper.Map<TDto>(await _repository.GetAsync(new(series, number)));
    }

    public async Task UpdateAsync(string series, uint number, TDto dto)
    {
        TEntity car = await _repository.GetAsync(new(series, number));
        car.Color = dto.Color;
        await _repository.UpdateAsync(car);
    }
}
