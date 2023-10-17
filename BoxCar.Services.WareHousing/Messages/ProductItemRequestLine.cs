using BoxCar.Services.WareHousing.Entities;

namespace BoxCar.Services.WareHousing.Messages
{
    public class ProductionRequestLineItem
    {
        public string Name { get; set; }

        public ItemType ItemType { get; set; }
       
        public Guid ItemTypeId { get; set; }

        public int Quantity { get; set; }

        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }

        public string SpecificationKey { get; set; }
    }
}
