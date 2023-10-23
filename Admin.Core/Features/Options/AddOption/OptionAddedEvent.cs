using BoxCar.Integration.Messages;

namespace BoxCar.Admin.Core.Features.Options.AddOption
{
    public class OptionAddedEvent : IntegrationBaseMessage
    {
        public Guid OptionId { get; set; }

        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;

        public int Price { get; set; }
    }
}
