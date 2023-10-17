using BoxCar.Catalogue.Core.Contracts.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoxCar.Catalogue.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IChassisAzServiceBusConsumer ChassisAzServiceBusConsumer { get; private set; }
        public static IVehicleAzServiceBusConsumer VehicleAzServiceBusConsumer { get; private set; }
        public static IOptionPackAzServiceBusConsumer OptionPackAzServiceBusConsumer { get; private set; }
        public static IEngineAzServiceBusConsumer EngineAzServiceBusConsumer { get; private set; }

        public static IApplicationBuilder UseAzServiceBusConsumer(this IApplicationBuilder app)
        {
            ChassisAzServiceBusConsumer = app.ApplicationServices.GetService<IChassisAzServiceBusConsumer>();
            EngineAzServiceBusConsumer = app.ApplicationServices.GetService<IEngineAzServiceBusConsumer>();
            OptionPackAzServiceBusConsumer = app.ApplicationServices.GetService<IOptionPackAzServiceBusConsumer>();
            VehicleAzServiceBusConsumer = app.ApplicationServices.GetService<IVehicleAzServiceBusConsumer>();
            var hostApplicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            hostApplicationLifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            ChassisAzServiceBusConsumer?.Start();
            VehicleAzServiceBusConsumer?.Start();
            EngineAzServiceBusConsumer?.Start();
            OptionPackAzServiceBusConsumer?.Start();
        }

        private static void OnStopping()
        {
            ChassisAzServiceBusConsumer?.Stop();
            VehicleAzServiceBusConsumer?.Stop();
            EngineAzServiceBusConsumer?.Stop();
            OptionPackAzServiceBusConsumer?.Stop();
        }
    }
}
