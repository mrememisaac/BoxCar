namespace Admin.Core.Features.OptionPacks.AddOptionPack
{
    public class AddOptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

    }
}
