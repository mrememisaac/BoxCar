using System.ComponentModel.DataAnnotations;

namespace BoxCar.ShoppingBasket.Models
{
    public class BasketLineForUpdate
    {
        [Required]
        public int Quantity { get; set; }
    }
}
