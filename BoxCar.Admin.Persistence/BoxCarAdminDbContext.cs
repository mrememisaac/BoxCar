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

            var option1 = new Option(Guid.NewGuid(), "Color", "Black", 500);
            var option2 = new Option(Guid.NewGuid(), "Seat Material", "Fabric", 600);
            var option3 = new Option(Guid.NewGuid(), "Rear View Camera", "true", 700);
            var option4 = new Option(Guid.NewGuid(), "Flood Lights", "true", 400);
            
            var optionPack1 = new OptionPack(Guid.NewGuid(), "Standard");
            var optionPack2 = new OptionPack(Guid.NewGuid(), "Deluxe");

            var engine1 = new Engine(Guid.NewGuid(), "Electric Car", FuelType.Electricity, IgnitionMethod.ElectricMotor, 0, 2000);
            var engine2 = new Engine(Guid.NewGuid(), "Diesel Car", FuelType.Diesel, IgnitionMethod.Compression, 0, 4000);

            var chassis1 = new Chassis(Guid.NewGuid(), "Simple Chassis", "This is the standard chassis ", 1000);
            var chassis2 = new Chassis(Guid.NewGuid(), "Enhanced Chassis", "This chassis has extra protection", 2000);
            
            
            modelBuilder.Entity<Option>().HasData(option1);
            modelBuilder.Entity<Option>().HasData(option2);
            modelBuilder.Entity<Option>().HasData(option3);
            modelBuilder.Entity<Option>().HasData(option4);

            modelBuilder.Entity<Chassis>().HasData(chassis1);
            modelBuilder.Entity<Chassis>().HasData(chassis2);

            modelBuilder.Entity<Engine>().HasData(engine1);
            modelBuilder.Entity<Engine>().HasData(engine2);
            
            modelBuilder.Entity<OptionPack>().HasData(optionPack1);
            modelBuilder.Entity<OptionPack>().HasData(optionPack2);


            var vehicle1 = new Vehicle(Guid.NewGuid(), "Vehicle 1", engine1.Id, chassis1.Id, optionPack1.Id, 5000);
            var vehicle2 = new Vehicle(Guid.NewGuid(), "Vehicle 2", engine2.Id, chassis2.Id, optionPack2.Id, 6000);

            modelBuilder.Entity<Vehicle>().HasData(vehicle1);
            modelBuilder.Entity<Vehicle>().HasData(vehicle2);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
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
        }
    }

}
