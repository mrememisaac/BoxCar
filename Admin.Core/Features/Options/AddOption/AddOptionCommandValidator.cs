using FluentValidation;

namespace BoxCar.Admin.Core.Features.Options.AddOption
{
    public class AddOptionCommandValidator : AbstractValidator<AddOptionCommand>
    {
        public AddOptionCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
