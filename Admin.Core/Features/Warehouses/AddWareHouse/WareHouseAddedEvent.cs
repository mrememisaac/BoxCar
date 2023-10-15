using BoxCar.Integration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Core.Features.Warehouses.AddWareHouse
{

    public class WareHouseAddedEvent : IntegrationBaseMessage
    {
        public Guid WareHouseId { get; set; }

        public string Name { get; set; } = null!;

        public AddressDto Address { get; set; }
    }
}
