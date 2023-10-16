using System.ComponentModel.DataAnnotations;

namespace BoxCar.Ordering.Entities
{
    public class OrderLine
    {
        public Guid VehicleId { get; set; }

        public Guid EngineId { get; set; }

        public Guid ChassisId { get; set; }

        public Guid OptionPackId { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public Order Order { get; set; }
    }
}
