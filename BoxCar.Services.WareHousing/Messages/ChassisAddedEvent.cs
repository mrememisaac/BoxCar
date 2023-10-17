using BoxCar.Integration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Services.WareHousing.Messages
{
    public class ChassisAddedEvent
    {
        public Guid Id { get; set; }
        
        public Guid ChassisId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        public int Price { get; set; }
    }
}
