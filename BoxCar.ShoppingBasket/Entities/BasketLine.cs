using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoxCar.ShoppingBasket.Entities
{
    public class BasketLine
    {
        public Guid BasketLineId { get; set; }

        [Required]
        public Guid BasketId { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
        
        [Required]
        public Guid EngineId { get; set; }

        
        [Required]
        public Guid ChassisId { get; set; }
        

        [Required]
        public Guid OptionPackId { get; set; }


        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UnitPrice { get; set; }

        public Basket Basket { get; set; }
    }
}
