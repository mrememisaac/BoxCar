
namespace BoxCar.Admin.Core.Features.Options.ListOptions
{
    public class OptionQueryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

        public int Price { get; set; }

    }
}
