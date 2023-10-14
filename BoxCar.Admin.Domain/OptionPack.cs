namespace BoxCar.Admin.Domain
{
    public class OptionPack : BaseEntity<Guid>
    {
        public string Name { get; set; } = null!;

        private List<Option> _options = new List<Option>();

        public IEnumerable<Option> Options => _options.ToList();

        public OptionPack(string name)
        {
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
            Name = newName ?? throw new ArgumentNullException(nameof(name));
        }
    }
}