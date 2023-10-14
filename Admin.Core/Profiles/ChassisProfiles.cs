using BoxCar.Admin.Core.Features.Chasis.AddChassis;
using BoxCar.Admin.Core.Features.Chasis.GetChassis;
using BoxCar.Admin.Core.Features.Engines.GetEngine;
using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;

namespace BoxCar.Admin.Core.Profiles
{
    public class ChassisProfiles : Profile
    {
        public ChassisProfiles()
        {
            CreateMap<AddChassisDto, AddChassisCommand>().ReverseMap();
            CreateMap<Chassis, AddChassisCommand>().ReverseMap();
            CreateMap<AddChassisResponse, Chassis>().ReverseMap();
            CreateMap<GetChassisByIdResponse, Chassis>().ReverseMap();
        }
    }
}
