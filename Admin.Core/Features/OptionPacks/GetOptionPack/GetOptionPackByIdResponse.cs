using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;

namespace BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack
{
    public class GetOptionPackByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
       
        public List<AddOptionDto> Options = new List<AddOptionDto>();
    }
}
