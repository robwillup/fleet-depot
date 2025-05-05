using AutoMapper;
using FleetDepot.Types.Dtos;
using FleetDepot.Types.Models;
using static FleetDepot.Types.Models.Vehicle;

namespace FleetDepot.Core.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vehicle, VehicleDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        CreateMap<VehicleDto, Vehicle>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src =>
                Enum.Parse<VehicleType>(src.Type)));

        CreateMap<Vehicle, CarDto>();
        CreateMap<CarDto, Vehicle>();
        CreateMap<BusDto, Vehicle>();
        CreateMap<Vehicle, BusDto>();
        CreateMap<TruckDto, Vehicle>();
        CreateMap<Vehicle, TruckDto>();
        CreateMap<VehicleDto, CarDto>();
        CreateMap<CarDto, VehicleDto>();
        CreateMap<VehicleDto, BusDto>();
        CreateMap<BusDto, VehicleDto>();
        CreateMap<VehicleDto, TruckDto>();
        CreateMap<TruckDto, BusDto>();
    }
}
