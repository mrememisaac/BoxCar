using BoxCar.Catalogue.Core.Features.Chasis.GetChassis;
using BoxCar.Catalogue.Core.Features.Engines.GetEngine;
using BoxCar.Catalogue.Core.Features.OptionPacks.GetOptionPack;
using BoxCar.Catalogue.Core.Features.Vehicles.GetVehicle;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BoxCar.Catalogue.Core
{
    public static class CoreServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("RedisConnectionString");
            });
            services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());
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
