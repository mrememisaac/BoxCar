using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.Admin.Persistence
{
    public class VehicleRepository : BaseRepository<Vehicle, Guid>, IVehicleRepository
    {
        public VehicleRepository(BoxCarAdminDbContext dbContext) : base(dbContext)
        { }
        public async new Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Vehicles
                .Include(v => v.Chassis)
                .Include(v => v.Engine)
                .Include(v => v.OptionPack)
                .ThenInclude(pack => pack.Options)
                .FirstAsync(x => x.Id == id);
        }

        public new async Task<IReadOnlyList<Vehicle>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            page = page < 0 ? 0 : page;
            pageSize = pageSize < 0 ? 100 : pageSize;
            pageSize = pageSize > 500 ? 500 : pageSize;
            var result = await _dbContext.Vehicles
                .Include(v => v.Chassis)
                .Include(v => v.Chassis)
                .Include(v => v.Engine)
                .Include(v => v.OptionPack)
                .ThenInclude(pack => pack.Options)
                .Skip(page * pageSize).Take(pageSize)
                .ToListAsync(cancellationToken);
            return result.AsReadOnly();

        }
    }
}
