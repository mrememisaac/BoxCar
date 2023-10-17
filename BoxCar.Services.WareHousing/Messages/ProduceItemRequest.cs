using BoxCar.Integration.Messages;

namespace BoxCar.Services.WareHousing.Messages
{
    public class ProductionRequest : IntegrationBaseMessage
    {
        public Guid OrderId { get; set; }

        public List<ProductionRequestLineItem> Items { get; set; } = new();
    }
}
