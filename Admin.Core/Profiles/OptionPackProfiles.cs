using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack;
using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;
using BoxCar.Admin.Core.Features.Factories.AddFactory;

namespace BoxCar.Admin.Core.Profiles
{
    public class OptionPackProfiles : Profile
    { 
        public OptionPackProfiles()
        {
            CreateMap<AddOptionPackDto, AddOptionPackCommand>().ReverseMap();
            CreateMap<AddOptionPackCommand, OptionPack>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AddOptionPackResponse, OptionPack>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<GetOptionPackByIdResponse, OptionPack>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<OptionPack, OptionPackDto>();
            CreateMap<OptionPack, Features.Vehicles.AddVehicle.OptionPackDto>();
            CreateMap<OptionPack, OptionPackAddedEvent>()
                .ForMember(d => d.OptionPackId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
