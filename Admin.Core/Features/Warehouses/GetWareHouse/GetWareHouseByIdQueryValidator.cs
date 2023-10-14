using FluentValidation;

namespace BoxCar.Admin.Core.Features.Warehouses.GetWareHouse
{

    public class GetWareHouseByIdQueryValidator : AbstractValidator<GetWareHouseByIdQuery>
    {
        public GetWareHouseByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
