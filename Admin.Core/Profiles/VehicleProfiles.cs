using BoxCar.Admin.Core.Features.Vehicles.AddVehicle;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;
using AutoMapper;
using BoxCar.Admin.Domain;

namespace BoxCar.Admin.Core.Profiles
{
    public class VehicleProfiles : Profile
    {
        public VehicleProfiles()
        {
            CreateMap<AddVehicleDto, AddVehicleCommand>().ReverseMap();
            CreateMap<Vehicle, AddVehicleCommand>().ReverseMap();
            CreateMap<AddVehicleResponse, Vehicle>().ReverseMap();
            CreateMap<GetVehicleByIdResponse, Vehicle>().ReverseMap();

        }
    }
}
