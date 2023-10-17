using BoxCar.Integration.Messages;

namespace BoxCar.Services.WareHousing.Messages
{
    public class OptionPackAddedEvent
    {
        public Guid OptionPackId { get; set; }

        public string Name { get; set; } = null!;

        public List<OptionDto> Options = new List<OptionDto>();

    }
}
