namespace BoxCar.Ordering.Messaging
{
    public interface IAzServiceBusConsumer
    {
        void Start();
        void Stop();
    }
}