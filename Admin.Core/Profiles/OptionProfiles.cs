using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;

namespace BoxCar.Admin.Core.Profiles
{
    public class OptionProfiles : Profile
    {
        public OptionProfiles()
        {
            CreateMap<OptionDto, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Features.Vehicles.AddVehicle.OptionDto, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AddOptionDto, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Option, Features.Vehicles.ListVehicles.OptionQueryItem>();
            CreateMap<Option, Features.OptionPacks.ListOptionPacks.OptionQueryItem>();
        }
    }
}
