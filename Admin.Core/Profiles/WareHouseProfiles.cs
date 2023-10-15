using BoxCar.Admin.Core.Features.Warehouses.AddWareHouse;
using BoxCar.Admin.Core.Features.Warehouses.GetWareHouse;
using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features.Factories.AddFactory;

namespace BoxCar.Admin.Core.Profiles
{
    public class WareHouseProfiles : Profile
    {
        public WareHouseProfiles()
        {
            CreateMap<AddWareHouseDto, AddWareHouseCommand>().ReverseMap();
            CreateMap<AddWareHouseCommand, WareHouse>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AddWareHouseResponse, WareHouse>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<GetWareHouseByIdResponse, WareHouse>()
                .ForMember(d => d.CreatedBy, opt => opt.Ignore())
                .ForMember(d => d.CreatedDate, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<WareHouse, WareHouseAddedEvent>()
                .ForMember(d => d.WareHouseId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
