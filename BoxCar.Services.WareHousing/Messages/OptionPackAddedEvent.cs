using BoxCar.Integration.Messages;

namespace BoxCar.Services.WareHousing.Messages
{
    public class OptionPackAddedEvent : IntegrationBaseMessage
    {
        public Guid OptionPackId { get; set; }

        public string Name { get; set; } = null!;

        public List<AddOptionDto> Options = new List<AddOptionDto>();

    }
}
