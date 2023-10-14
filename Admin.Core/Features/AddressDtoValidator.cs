using BoxCar.Admin.Core.Features.Factories.AddFactory;
using FluentValidation;

namespace BoxCar.Admin.Core.Features
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(p => p.Street).NotEmpty().MaximumLength(50);
            RuleFor(p => p.City).NotEmpty().MaximumLength(50);
            RuleFor(p => p.PostalCode).NotEmpty().MaximumLength(10);
            RuleFor(p => p.State).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Country).NotEmpty().MaximumLength(50);
        }
    }
}
