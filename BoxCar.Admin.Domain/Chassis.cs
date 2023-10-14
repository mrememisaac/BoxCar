namespace BoxCar.Admin.Domain
{
    public class Chassis : BaseEntity<Guid>
    {
        public string Name { get; private set; } = null!;

        public string Description { get; private set; } = string.Empty;

        public Chassis(string name, string description)
        {
            ChangeName(name);
            ChangeDescription(description);
        }

        public void ChangeName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void ChangeDescription(string description) { 
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}