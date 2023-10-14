using Admin.Core.Features.Factories.AddFactory;
using Admin.Core.Features.Factories.GetFactory;
using AutoMapper;
using BoxCar.Admin.Domain;

namespace Admin.Core.Profiles
{
    public class FactoryProfiles : Profile
    {
        public FactoryProfiles()
        {
            CreateMap<AddFactoryDto, AddFactoryCommand>().ReverseMap();
            CreateMap<Factory, AddFactoryCommand>().ReverseMap();
            CreateMap<AddFactoryResponse, Factory>().ReverseMap();
            CreateMap<GetFactoryByIdResponse, Factory>().ReverseMap();
        }
    }
}
