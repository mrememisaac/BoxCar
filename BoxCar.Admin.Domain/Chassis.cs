namespace BoxCar.Admin.Domain
{
    public class Chassis : Entity
    {
        public string Name { get; private set; } = null!;

        public string Description { get; private set; } = string.Empty;

        public int Price { get; private set; }
        
        public Chassis(Guid id, string name, string description, int price)
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id; 
            ChangeName(name);
            ChangeDescription(description);
            Price = price;
        }

        public List<Vehicle> Vehicles { get; } = new();

        public void ChangeName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void ChangeDescription(string description) { 
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        private Chassis()
        {

        }
    }
}