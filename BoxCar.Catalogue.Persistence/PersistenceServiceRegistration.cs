﻿using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Domain;
using BoxCar.Catalogue.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoxCar.Catalogue.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BoxCarDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IChassisRepository, ChassisRepository>();
            services.AddScoped<IEngineRepository, EngineRepository>();
            services.AddScoped<IOptionPackRepository, OptionPackRepository>();

            return services;
        }
    }
}
