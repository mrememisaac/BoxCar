namespace BoxCar.Admin.Domain
{
    public class OptionPack
    {
        public string Name { get; set; } = null!;

        private List<Option> _options = new List<Option>();

        public IEnumerable<Option> Options => _options.ToList();

        public OptionPack(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AddOption(Option option)
        {
            _options.Add(option);
        }

        public bool RemoveOption(Option option)
        {
            return _options.Remove(option);
        }
    }
}