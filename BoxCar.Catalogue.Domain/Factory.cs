namespace BoxCar.Catalogue.Domain
{
    public class Factory : Entity
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public Factory(Guid id, string name, Address address)
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }
}