using System.ComponentModel.DataAnnotations;

namespace BoxCar.Catalogue.Domain
{
    public class WareHouse : Entity
    {
        public string Name { get; private set; }

        public Address Address { get; set; }

        public WareHouse(Guid id, string name, Address address)
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }

    public class BasketLine
    {
        public Guid BasketLineId { get; set; }

        [Required]
        public Guid BasketId { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        [Required]
        public int TicketAmount { get; set; }

        [Required]
        public int Price { get; set; }

        public Basket Basket { get; set; }
    }
}