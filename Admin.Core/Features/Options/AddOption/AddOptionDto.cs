namespace BoxCar.Admin.Core.Features.Options.AddOption
{
    public class AddOptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

        public int Price { get; set; }

    }
}
