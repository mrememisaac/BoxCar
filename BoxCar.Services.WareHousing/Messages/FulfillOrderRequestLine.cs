

namespace BoxCar.Services.WareHousing.Messages
{
    public class FulfillOrderRequestLine
    {
        public Guid VehicleId { get; set; }

        public Guid EngineId { get; set; }

        public Guid ChassisId { get; set; }

        public Guid OptionPackId { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }
    }
}
