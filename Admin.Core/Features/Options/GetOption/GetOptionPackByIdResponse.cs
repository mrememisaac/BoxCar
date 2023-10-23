using BoxCar.Admin.Core.Features.Options.AddOption;

namespace BoxCar.Admin.Core.Features.Options.GetOption
{
    public class GetOptionByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;

        public int Price { get; set; }

    }
}
