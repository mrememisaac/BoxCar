namespace BoxCar.Admin.Domain
{
    public class OptionPack : Entity
    {
        public string Name { get; set; } = null!;

        private List<Option> _options = new List<Option>();

        public IEnumerable<Option> Options => _options.ToList();

        public List<Vehicle> Vehicles { get; } = new();

        public OptionPack(Guid id, string name)
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id;
            Rename(name);
        }

        public void AddOption(Option option)
        {
            _options.Add(option);
        }

        public bool RemoveOption(Option option)
        {
            return _options.Remove(option);
        }

        public void Rename(string newName)
        {
            Name = newName ?? throw new ArgumentNullException(nameof(newName));
        }
    }
}