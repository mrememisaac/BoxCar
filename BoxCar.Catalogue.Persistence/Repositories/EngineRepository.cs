using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.Catalogue.Persistence.Repositories
{
    public class EngineRepository : BaseRepository<Engine, Guid>, IEngineRepository
    {
        public EngineRepository(DbContextOptions<BoxCarDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
