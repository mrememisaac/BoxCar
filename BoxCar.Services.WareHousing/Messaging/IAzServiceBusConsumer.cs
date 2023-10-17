namespace BoxCar.Services.WareHousing.Messaging
{
    public interface IAzServiceBusConsumer
    {
        void Start();
        void Stop();
    }
}