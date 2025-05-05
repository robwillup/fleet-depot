using FleetDepot.Types.Dtos;
using FleetDepot.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetDepot.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController(IVehicleService<VehicleDto> service) : VehiclesControllerBase<VehicleDto>(service)
{
    public override Task<IActionResult> PostAsync(VehicleDto dto)
    {
        return Task.FromResult<IActionResult>(BadRequest(new { message = "Cannot create a vehicle from this endpoint." }));
    }
}
