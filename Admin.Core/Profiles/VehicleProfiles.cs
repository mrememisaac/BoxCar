using BoxCar.Admin.Core.Features.Vehicles.AddVehicle;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;
using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features.Factories.AddFactory;

namespace BoxCar.Admin.Core.Profiles
{
    public class VehicleProfiles : Profile
    {
        public VehicleProfiles()
        {
            CreateMap<AddVehicleDto, AddVehicleCommand>().ReverseMap();
            CreateMap<Vehicle, GetVehicleByIdResponse>();
            CreateMap<Vehicle, AddVehicleResponse>();
            CreateMap<Vehicle, VehicleAddedEvent>()
                .ForMember(d => d.CreationDateTime, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(d => d.VehicleId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Vehicle, Features.Vehicles.ListVehicles.VehicleQueryItem>();
        }
    }
}
