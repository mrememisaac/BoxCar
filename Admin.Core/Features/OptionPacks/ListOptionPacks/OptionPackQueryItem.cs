
namespace BoxCar.Admin.Core.Features.OptionPacks.ListOptionPacks
{
    public class OptionPackQueryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public List<OptionQueryItem> Options = new List<OptionQueryItem>();
    }
}
