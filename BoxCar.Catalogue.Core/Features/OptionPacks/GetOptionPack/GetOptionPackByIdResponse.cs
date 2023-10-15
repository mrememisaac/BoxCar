
namespace BoxCar.Catalogue.Core.Features.OptionPacks.GetOptionPack
{
    public class GetOptionPackByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
       
        public List<OptionDto> Options = new List<OptionDto>();
    }
}
