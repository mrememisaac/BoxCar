using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.Catalogue.Persistence.Repositories
{
    public class OptionPackRepository : BaseRepository<OptionPack, Guid>, IOptionPackRepository
    {
        public OptionPackRepository(DbContextOptions<BoxCarDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
