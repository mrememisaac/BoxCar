using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BoxCar.Admin.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BoxCarAdminDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IAsyncRepository<,>), typeof(BaseRepository<,>));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
