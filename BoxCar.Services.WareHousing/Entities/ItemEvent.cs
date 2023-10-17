namespace BoxCar.Services.WareHousing.Entities
{
    public class ItemEvent
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }
    }
}
