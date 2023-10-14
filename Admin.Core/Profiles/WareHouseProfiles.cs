using BoxCar.Admin.Core.Features.Warehouses.AddWareHouse;
using BoxCar.Admin.Core.Features.Warehouses.GetWareHouse;
using AutoMapper;
using BoxCar.Admin.Domain;

namespace Admin.Core.Profiles
{
    public class WareHouseProfiles : Profile
    {
        public WareHouseProfiles()
        {
            CreateMap<AddWareHouseDto, AddWareHouseCommand>().ReverseMap();
            CreateMap<WareHouse, AddWareHouseCommand>().ReverseMap();
            CreateMap<AddWareHouseResponse, WareHouse>().ReverseMap();
            CreateMap<GetWareHouseByIdResponse, WareHouse>().ReverseMap();
        }
    }
}
