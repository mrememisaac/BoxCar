using BoxCar.Integration.Messages;

namespace BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack
{
    public class OptionPackAddedEvent : IntegrationBaseMessage
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public List<AddOptionDto> Options = new List<AddOptionDto>();
    }
}
