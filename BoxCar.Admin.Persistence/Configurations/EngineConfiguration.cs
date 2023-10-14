using BoxCar.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoxCar.Admin.Persistence.Configurations
{
    public class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.UpdatedBy).HasMaxLength(50);
            builder.Property(p => p.FuelType).HasConversion<string>();
            builder.Property(p => p.IgnitionMethod).HasConversion<string>();
        }
    }
}
