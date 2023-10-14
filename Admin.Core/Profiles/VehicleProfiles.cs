using Admin.Core.Features.Vehicles.AddVehicle;
using Admin.Core.Features.Vehicles.GetVehicle;
using AutoMapper;
using BoxCar.Admin.Domain;

namespace Admin.Core.Profiles
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
