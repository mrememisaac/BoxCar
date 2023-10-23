namespace BoxCar.ShoppingBasket.Messaging.Messages
{
    public class ChassisAddedEvent
    {
        public Guid Id { get; set; }

        public Guid ChassisId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        public int Price { get; set; }
    }
}
