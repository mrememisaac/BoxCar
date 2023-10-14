using BoxCar.Admin.Domain;

namespace BoxCar.Admin.Core.Features.Factories.GetFactory
{
    public class GetFactoryByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

    }
}
