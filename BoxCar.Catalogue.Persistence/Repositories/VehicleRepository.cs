using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.Catalogue.Persistence.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle, Guid>, IVehicleRepository
    {
        public VehicleRepository(DbContextOptions<BoxCarCatalogueDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public async new Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var _dbContext = new BoxCarCatalogueDbContext(_dbContextOptions);
            return await _dbContext.Vehicles
                .Include(v => v.Chassis)
                .Include(v => v.Engine)
                .Include(v => v.OptionPack)
                .ThenInclude(pack => pack.Options)
                .FirstAsync(x => x.Id == id);
        }
    }
}
