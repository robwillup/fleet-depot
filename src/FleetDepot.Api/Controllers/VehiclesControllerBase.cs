using FleetDepot.Types.Dtos;
using FleetDepot.Types.Exceptions;
using FleetDepot.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetDepot.Api.Controllers;

public abstract class VehiclesControllerBase<TDto>(IVehicleService<TDto> vehicleService) : ControllerBase
    where TDto : VehicleDto
{
    protected readonly IVehicleService<TDto> _vehicleService = vehicleService;

    [HttpGet]
    public async Task<IEnumerable<TDto>> GetAsync()
    {
        return await _vehicleService.GetAllAsync();
    }

    [HttpGet("{series}/{number}")]
    public async Task<TDto> GetAsync(string series, uint number)
    {
        return await _vehicleService.GetByChassisIdAsync(series, number);
    }

    [HttpPost]
    public virtual async Task<IActionResult> PostAsync(TDto dto)
    {
        try
        {
            VehicleDto vehicle = await _vehicleService.AddAsync(dto);
            return Created($"/vehicles/{dto.Type.ToLower()}/{dto.Series}-{dto.Number}", dto);
        }
        catch (DuplicateChassisIdException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{series}/{number}")]
    public async Task<IResult> PutAsync(string series, uint number, [FromBody] TDto dto)
    {
        await _vehicleService.UpdateAsync(series, number, dto);
        return Results.Ok();
    }

    [HttpDelete("{series}/{number}")]
    public async Task<IResult> DeleteAsync(string series, uint number)
    {
        await _vehicleService.DeleteAsync(series, number);
        return Results.Ok();
    }
}
