using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack;
using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;

namespace Admin.Core.Profiles
{
    public class OptionPackProfiles : Profile
    { 
        public OptionPackProfiles()
        {
            CreateMap<AddOptionPackDto, AddOptionPackCommand>().ReverseMap();
            CreateMap<OptionPack, AddOptionPackCommand>().ReverseMap();
            CreateMap<AddOptionPackResponse, OptionPack>().ReverseMap();
            CreateMap<GetOptionPackByIdResponse, OptionPack>().ReverseMap();
        }
    }
}
