using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.Services.ShoppingBasket.Messages
{
    public class BasketLineMessage
    {
        public Guid BasketLineId { get; set; }
        
        public Guid VehicleId { get; set; }
        
        public Guid EngineId { get; set; }

        public Guid ChassisId { get; set; }

        public Guid OptionPackId { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
