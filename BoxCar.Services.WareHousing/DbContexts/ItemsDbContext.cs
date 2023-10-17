using BoxCar.Services.WareHousing.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxCar.Services.WareHousing.DbContexts
{
    public class ItemsDbContext : DbContext
    {
        public ItemsDbContext(DbContextOptions<ItemsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        
    }
}
