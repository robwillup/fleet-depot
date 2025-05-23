﻿using FleetDepot.Types.Dtos;
using FleetDepot.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetDepot.Api.Controllers;

[Route("api/vehicles/[controller]")]
[ApiController]
public class TrucksController(IVehicleService<TruckDto> service) : VehiclesControllerBase<TruckDto>(service) { }
