using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;
using BoxCar.Admin.Core.Features.Options.AddOption;
using BoxCar.Admin.Core.Features.Options.GetOption;

namespace BoxCar.Admin.Core.Profiles
{
    public class OptionProfiles : Profile
    {
        public OptionProfiles()
        {
            CreateMap<Features.Options.AddOption.AddOptionDto, AddOptionCommand>().ReverseMap();
            CreateMap<AddOptionCommand, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AddOptionResponse, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<GetOptionByIdResponse, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<OptionDto, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.OptionPacks, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Features.Vehicles.AddVehicle.OptionDto, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.OptionPacks, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Features.OptionPacks.AddOptionPack.AddOptionDto, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.OptionPacks, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Features.Options.AddOption.AddOptionDto, Option>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForMember(d => d.OptionPacks, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Option, Features.Vehicles.ListVehicles.OptionQueryItem>();
            CreateMap<Option, Features.OptionPacks.ListOptionPacks.OptionQueryItem>();
            CreateMap<Option, OptionAddedEvent>()
                .ForMember(d => d.CreationDateTime, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(d => d.OptionId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Option, Features.Options.ListOptions.OptionQueryItem>();

        }
    }
}
