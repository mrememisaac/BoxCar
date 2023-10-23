using FluentValidation;

namespace BoxCar.Admin.Core.Features.Options.GetOption
{
    public class GetOptionByIdQueryValidator : AbstractValidator<GetOptionByIdQuery>
    {
        public GetOptionByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
