using Admin.Core.Features.Chasis.AddChassis;
using Admin.Core.Features.Chasis.GetChassis;
using Admin.Core.Features.Engines.GetEngine;
using AutoMapper;
using BoxCar.Admin.Domain;

namespace Admin.Core.Profiles
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
