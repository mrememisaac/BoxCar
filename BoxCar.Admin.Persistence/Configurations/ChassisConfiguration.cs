using BoxCar.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoxCar.Admin.Persistence.Configurations
{
    public class ChassisConfiguration : IEntityTypeConfiguration<Chassis>
    {
        public void Configure(EntityTypeBuilder<Chassis> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.UpdatedBy).HasMaxLength(50);
        }
    }
}
