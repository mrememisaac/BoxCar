namespace BoxCar.Catalogue.Domain
{
    public class IntegrationEvent : Entity
    {
        public int Id { get; set; }
        public string EventType { get; set; }
        public string ServiceBusTopicName { get; set; }
        public string EventBody { get; set; }
        public string State { get; set; }
    }
}