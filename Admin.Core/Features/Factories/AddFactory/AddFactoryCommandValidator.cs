using FluentValidation;

namespace BoxCar.Admin.Core.Features.Factories.AddFactory
{
    public class AddFactoryCommandValidator : AbstractValidator<AddFactoryCommand>
    {
        public AddFactoryCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name)
                .MaximumLength(250)
                .NotEmpty();
            RuleFor(p => p.Address).SetValidator(new AddressDtoValidator());
        }
    }
}
