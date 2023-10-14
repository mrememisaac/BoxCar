namespace BoxCar.Admin.Domain
{
    public class Factory : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public Factory(string name, Address address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }
}