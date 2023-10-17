using BoxCar.Integration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Core.Features.Chasis.AddChassis
{

    public class ChassisAddedEvent : IntegrationBaseMessage
    {
        public Guid ChassisId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        public int Price { get; set; }

    }
}
