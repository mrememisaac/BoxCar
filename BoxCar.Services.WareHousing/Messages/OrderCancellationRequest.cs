namespace BoxCar.Services.WareHousing.Messages
{
    public class OrderCancellationRequest
    {
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }
    }
}
