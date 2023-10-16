using System;

namespace BoxCar.ShoppingBasket.Entities
{
    public class BasketChangeEvent
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EngineId { get; set; }
        public Guid ChassisId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid OptionPackId { get; set; }
        public DateTimeOffset InsertedAt { get; set; }
        public BasketChangeTypeEnum BasketChangeType { get; set; }
    }
}
