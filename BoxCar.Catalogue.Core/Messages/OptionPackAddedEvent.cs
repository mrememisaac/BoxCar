using BoxCar.Integration.Messages;

namespace BoxCar.Catalogue.Messages
{
    public class OptionPackAddedEvent 
    {
        public Guid Id { get; set; }

        public Guid OptionPackId { get; set; }

        public string Name { get; set; } = null!;

        
        public List<OptionDto> Options = new List<OptionDto>();

    }
}
