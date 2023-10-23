using MediatR;

namespace BoxCar.Admin.Core.Features.Options.AddOption
{
    public class AddOptionCommand : IRequest<Result<AddOptionResponse>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Value { get; set; } = string.Empty;

    }
}
