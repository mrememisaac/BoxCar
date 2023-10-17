namespace BoxCar.Services.WareHousing.Contracts.Messaging
{
    public interface IAzServiceBusConsumer
    {
        void Start();
        void Stop();
    }
}