using BoxCar.Integration.Messages;

namespace BoxCar.Admin.Core.Features.Factories.AddFactory
{
    public class FactoryAddedEvent : IntegrationBaseMessage
    {
        public Guid FactoryId { get; set; }

        public string Name { get; set; } = null!;

        public AddressDto Address { get; set; }

    }
}
