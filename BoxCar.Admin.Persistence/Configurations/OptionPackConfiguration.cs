using BoxCar.Admin.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BoxCar.Admin.Persistence.Configurations
{

    public class OptionPackConfiguration : IEntityTypeConfiguration<OptionPack>
    {
        public void Configure(EntityTypeBuilder<OptionPack> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.UpdatedBy).HasMaxLength(50);
        }
    }
}
