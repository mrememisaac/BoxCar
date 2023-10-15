using BoxCar.Catalogue.Core.Features.Chasis.GetChassis;
using BoxCar.Catalogue.Core.Features.Engines.GetEngine;
using BoxCar.Catalogue.Core.Features.OptionPacks.GetOptionPack;
using BoxCar.Catalogue.Core.Features.Vehicles.GetVehicle;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BoxCar.Catalogue.Core
{
    public static class CoreServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<GetVehicleByIdQueryValidator>();
            services.AddSingleton<GetOptionPackByIdQueryValidator>();
            services.AddSingleton<GetEngineByIdQueryValidator>();
            services.AddSingleton<GetChassisByIdQueryValidator>();
            return services;
        }
    }
}
