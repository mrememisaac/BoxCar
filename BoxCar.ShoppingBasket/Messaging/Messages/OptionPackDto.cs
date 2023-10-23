namespace BoxCar.ShoppingBasket.Messaging.Messages
{
    public class OptionPackDto
    {
        public Guid Id { get; set; }

        public Guid OptionPackId { get; set; }

        public string Name { get; set; } = null!;

        public List<OptionDto> Options = new List<OptionDto>();
    }
}
