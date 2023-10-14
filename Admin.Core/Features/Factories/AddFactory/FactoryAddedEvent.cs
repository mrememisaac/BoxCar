using BoxCar.Integration.Messages;

namespace BoxCar.Admin.Core.Features.Factories.AddFactory
{
    public class FactoryAddedEvent : IntegrationBaseMessage
    {
        public string Name { get; private set; } = null!;

    }
}
