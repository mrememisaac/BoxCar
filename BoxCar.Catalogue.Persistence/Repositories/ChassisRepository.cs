using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.Catalogue.Persistence.Repositories
{
    public class ChassisRepository : BaseRepository<Chassis, Guid>, IChassisRepository
    {
        public ChassisRepository(DbContextOptions<BoxCarDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
