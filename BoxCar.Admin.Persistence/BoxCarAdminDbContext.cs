using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxCar.Admin.Core.Contracts.Identity;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.Configuration;

namespace BoxCar.Admin.Persistence
{
    public class BoxCarAdminDbContext : DbContext
    {
        public BoxCarAdminDbContext() : base()
        {
        }
                
        public BoxCarAdminDbContext(DbContextOptions<BoxCarAdminDbContext> dbContextOptions) : base(dbContextOptions)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (!optionsBuilder.IsConfigured)
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }
        }

        public DbSet<Factory> Factories { get; set; }

        public DbSet<WareHouse> WareHouses { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<Chassis> Chassis { get; set; }
        
        public DbSet<OptionPack> OptionPacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoxCarAdminDbContext).Assembly);
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
                        entry.Entity.UpdatedBy = entry.Entity.UpdatedBy ?? "service";
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = entry.Entity.CreatedBy ?? "service";
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
