namespace BoxCar.Services.WareHousing.Messages
{
    public class OrderItemAvailabilityLine
    {
        public Guid OrderItemId { get; set;}

        public OrderItemAvailabilityStatus Status { get; set; }
    }
}
