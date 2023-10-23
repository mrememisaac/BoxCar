namespace BoxCar.Admin.Core.Features.Options.AddOption
{
    public class AddOptionResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;

        public int Price { get; set; }

    }
}
