using FluentValidation;

namespace BoxCar.Admin.Core.Features.Factories.GetFactory
{
    public class GetFactoryByIdQueryValidator : AbstractValidator<GetFactoryByIdQuery>
    {
        public GetFactoryByIdQueryValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
