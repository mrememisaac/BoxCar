using MediatR;

namespace BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack
{
    public class AddOptionPackCommand : IRequest<Result<AddOptionPackResponse>>
    {
        public Guid Id { get; set; }

        public string Name { get; private set; } = null!;

        public List<AddOptionDto> Options = new List<AddOptionDto>();
    }
}
