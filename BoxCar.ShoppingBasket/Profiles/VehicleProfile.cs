using AutoMapper;

namespace BoxCar.ShoppingBasket.Profiles
{
    public class VehicleProfile: Profile
    {
        public VehicleProfile()
        {
            CreateMap<Entities.Vehicle, Models.Vehicle>().ReverseMap();
            CreateMap<Entities.Chassis, Models.Chassis>().ReverseMap();
            CreateMap<Entities.Engine, Models.Engine>().ReverseMap();
            CreateMap<Entities.OptionPack, Models.OptionPack>().ReverseMap();
            CreateMap<Entities.Option, Models.Option>().ReverseMap();
        }
    }
}
