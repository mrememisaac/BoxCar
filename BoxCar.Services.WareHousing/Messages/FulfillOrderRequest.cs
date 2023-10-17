using BoxCar.Integration.Messages;

namespace BoxCar.Services.WareHousing.Messages
{
    public class FulfillOrderRequest
    {
        public Guid OrderId { get; set; }

        public List<FulfillOrderRequestLine> OrderItems { get; set; }
    }
}
