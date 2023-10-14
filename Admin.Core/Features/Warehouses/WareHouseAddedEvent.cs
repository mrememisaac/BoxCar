using BoxCar.Integration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Features.Factories.AddWareHouse
{

    public class WareHouseAddedEvent : IntegrationBaseMessage
    {
        public string Name { get; private set; } = null!;

        public string Description { get; private set; } = string.Empty;
    }
}
