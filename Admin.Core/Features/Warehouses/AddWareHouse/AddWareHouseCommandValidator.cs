using FluentValidation;

namespace BoxCar.Admin.Core.Features.Warehouses.AddWareHouse
{
    public class AddWareHouseCommandValidator : AbstractValidator<AddWareHouseCommand>
    {
        public AddWareHouseCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name)
                .MaximumLength(250)
                .NotEmpty();
            RuleFor(p => p.Address).SetValidator(new AddressDtoValidator());
        }
    }
}
