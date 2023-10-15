using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxCar.Catalogue.Core.Contracts.Identity;
using BoxCar.Catalogue.Domain;

namespace BoxCar.Catalogue.Persistence
{
    public class BoxCarDbContext : DbContext
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public BoxCarDbContext(DbContextOptions<BoxCarDbContext> dbContextOptions) : base(dbContextOptions)
        { }

        public BoxCarDbContext(DbContextOptions<BoxCarDbContext> dbContextOptions, ILoggedInUserService loggedInUserService) : base(dbContextOptions)
        {
            this._loggedInUserService = loggedInUserService;
        }

        public DbSet<Factory> Factories { get; set; }

        public DbSet<WareHouse> WareHouses { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<Chassis> Chassis { get; set; }
        
        public DbSet<OptionPack> OptionPacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxCarDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
