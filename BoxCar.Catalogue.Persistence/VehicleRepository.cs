using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.Catalogue.Persistence
{
    public class VehicleRepository : BaseRepository<Vehicle, Guid>, IVehicleRepository
    {
        public VehicleRepository(BoxCarDbContext dbContext) : base(dbContext)
        {

        }

        public async new Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Vehicles
                .Include(v => v.Chassis)
                .Include(v => v.Engine)
                .Include(v => v.OptionPack)
                .ThenInclude(pack => pack.Options)
                .FirstAsync(x => x.Id == id);
        }
    }
}
