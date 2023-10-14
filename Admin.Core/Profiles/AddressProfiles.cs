using AutoMapper;
using BoxCar.Admin.Domain;
using BoxCar.Admin.Core.Features;

namespace BoxCar.Admin.Core.Profiles
{
    public class AddressProfiles : Profile
    {
        public AddressProfiles()
        {
            CreateMap<AddressDto, Address>()
                .ConstructUsing(address => new Address(address.Street, address.City, address.State, address.PostalCode, address.Country))
                .ReverseMap();
        }
    }
}
