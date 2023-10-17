namespace BoxCar.Catalogue.Core.Contracts.Messaging
{
    public interface IAzServiceBusConsumer
    {
        void Start();
        void Stop();
    }
}