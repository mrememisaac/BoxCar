using BoxCar.Admin.Core.Features.Chasis.AddChassis;
using BoxCar.Admin.Core.Features.Chasis.GetChassis;
using BoxCar.Admin.Core.Features.Engines.GetEngine;
using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;
using BoxCar.Admin.Core.Features.Engines.AddEngine;
using BoxCar.Admin.Core.Features.Chasis.ListChassis;

namespace BoxCar.Admin.Core.Profiles
{
    public class ChassisProfiles : Profile
    {
        public ChassisProfiles()
        {
            CreateMap<AddChassisDto, AddChassisCommand>().ReverseMap();
            CreateMap<AddChassisCommand, Chassis>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Vehicles, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AddChassisResponse, Chassis>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Vehicles, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<GetChassisByIdResponse, Chassis>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Vehicles, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Features.Vehicles.AddVehicle.ChassisDto, Chassis>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Vehicles, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Features.Vehicles.GetVehicle.ChassisDto, Chassis>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.Vehicles, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Chassis, ChassisAddedEvent>()
                .ForMember(d => d.CreationDateTime, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(d => d.ChassisId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Chassis, Features.Chasis.ListChassis.ChassisQueryItem>();
            CreateMap<Chassis, Features.Vehicles.ListVehicles.ChassisQueryItem>();
        }
    }
}
