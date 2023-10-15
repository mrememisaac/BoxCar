using BoxCar.Admin.Core.Features.Factories.AddFactory;
using BoxCar.Admin.Core.Features.Factories.GetFactory;
using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features.Chasis.AddChassis;

namespace BoxCar.Admin.Core.Profiles
{
    public class FactoryProfiles : Profile
    {
        public FactoryProfiles()
        {
            CreateMap<AddFactoryDto, AddFactoryCommand>()
                .ReverseMap();
            CreateMap<AddFactoryCommand, Factory>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AddFactoryResponse, Factory>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<GetFactoryByIdResponse, Factory>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Factory, FactoryAddedEvent>()
                .ForMember(d => d.FactoryId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
