using FluentValidation;

namespace BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack
{
    public class AddOptionPackCommandValidator : AbstractValidator<AddOptionPackCommand>
    {
        public AddOptionPackCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty().MaximumLength(250);
            //RuleFor(p => p.Options)                .NotEmpty(); 
            RuleForEach(p => p.Options).SetValidator(new AddOptionDtoValidator());
        }
    }
}
