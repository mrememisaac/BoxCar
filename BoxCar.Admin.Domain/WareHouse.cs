namespace BoxCar.Admin.Domain
{
    public class WareHouse : BaseEntity<Guid>
    {
        public string Name { get; private set; }

        public Address Address { get; set; }

        public WareHouse(string name, Address address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }
}